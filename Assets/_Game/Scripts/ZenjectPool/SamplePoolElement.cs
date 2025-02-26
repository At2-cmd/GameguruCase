using UnityEngine;
using Zenject;

public class SamplePoolElement : MonoBehaviour
{
    private Pool _pool;

    public void Despawn()
    {
        _pool.Despawn(this);
    }

    private void SetPool(Pool pool)
    {
        _pool = pool;
    }

    private void SetPosition(Vector3 position)
    {
        transform.position = position;
    }

    public class Pool : MonoMemoryPool<Vector3, SamplePoolElement>
    {
        protected override void OnCreated(SamplePoolElement item)
        {
            base.OnCreated(item);
            item.SetPool(this);
        }

        protected override void OnDespawned(SamplePoolElement item)
        {
            base.OnDespawned(item);
        }

        protected override void OnDestroyed(SamplePoolElement item)
        {
            base.OnDestroyed(item);
        }

        protected override void OnSpawned(SamplePoolElement item)
        {
            base.OnSpawned(item);
        }

        protected override void Reinitialize(Vector3 position, SamplePoolElement item)
        {
            base.Reinitialize(position, item);
            item.SetPosition(position);
        }
    }
}
