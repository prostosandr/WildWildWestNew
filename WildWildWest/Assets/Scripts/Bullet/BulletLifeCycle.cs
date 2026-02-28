using System;
using System.Collections;
using UnityEngine;

public class BulletLifeCycle : MonoBehaviour
{
    private float _damage;
    private float _lifeTime;
    private WaitForSeconds _wait;
    private Coroutine _lifeCycle;

    public event Action Deactivated;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Character character))
        {
            Vector3 position = other.ClosestPoint(transform.position);

            Vector3 direction = transform.position - position;

            Quaternion rotation;

            if (direction != Vector3.zero)
                rotation = Quaternion.LookRotation(direction);
            else
                rotation = transform.rotation;

            character.TakeDamage(_damage, position, rotation);
        }

        Deactivate();
    }

    public void Initialize(float lifeTime, float damage)
    {
        _lifeTime = lifeTime;
        _damage = damage;

        _wait = new WaitForSeconds(_lifeTime);

        _lifeCycle = StartCoroutine(StartLifeCycle(_wait));
    }

    private IEnumerator StartLifeCycle(WaitForSeconds wait)
    {
        yield return wait;

        Deactivate();
    }

    private void Deactivate()
    {
        if (_lifeCycle != null)
        {
            StopCoroutine(_lifeCycle);
            _lifeCycle = null;
        }

        Deactivated?.Invoke();
    }
}