using UnityEngine;
using UnityEngine.Pool;

public abstract class Spawner<TItem, IData> : MonoBehaviour where TItem : MonoBehaviour, IPolledObject<TItem>
{
    [SerializeField] protected Transform Container;
    [SerializeField] protected TItem Prefab;

    protected ObjectPool<TItem> Pool;
    protected TItem CurrentItem;

    public TItem CurrnetItem => CurrentItem;

    public void Initialize(Transform container, TItem prefab, int capacity, int maxSize)
    {
        Container = container;
        Prefab = prefab;

        Pool = new ObjectPool<TItem>(
            createFunc: () => CreateItem(),
            actionOnGet: (item) => ActionOnGet(item),
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
        var item = Instantiate(Prefab, Container);

        return item;
    }

    private void ActionOnGet(TItem item)
    {
        CurrentItem = item;
        item.Deactivated += ReleaseItem;
    }

    protected virtual void ReleaseItem(TItem item)
    {
        item.Deactivated -= ReleaseItem;

        Pool.Release(item);
    }
}