using UnityEngine;
using UnityEngine.Pool;

public abstract class Spawner<TItem, IData> : MonoBehaviour where TItem : MonoBehaviour, IPolledObject<TItem>
{
    protected Transform _container;
    protected TItem _prefab;

    protected ObjectPool<TItem> _pool;
    protected TItem _currentItem;

    public TItem CurrnetItem => _currentItem;

    public void Initialize(Transform container, TItem prefab, int capacity, int maxSize)
    {
        _container = container;
        _prefab = prefab;

        _pool = new ObjectPool<TItem>(
            createFunc: () => CreateItem(),
            actionOnGet: (item) => ActOnGet(item),
            actionOnRelease: (item) => item.gameObject.SetActive(false),
            actionOnDestroy: (item) => Destroy(item.gameObject),
            collectionCheck: true,
            defaultCapacity: capacity,
            maxSize: maxSize);
    }

    public virtual void Spawn(Transform spawnerPosition, IData data) { }
    public virtual void Spawn() { }

    private TItem CreateItem()
    {
        var item = Instantiate(_prefab, _container);

        return item;
    }

    private void ActOnGet(TItem item)
    {
        _currentItem = item;
        item.Deactivated += ReleaseItem;
    }

    protected virtual void ReleaseItem(TItem item)
    {
        item.Deactivated -= ReleaseItem;

        _pool.Release(item);
    }
}