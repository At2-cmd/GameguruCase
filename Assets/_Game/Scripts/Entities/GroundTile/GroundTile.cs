using UnityEngine;
using Zenject;

public class GroundTile : MonoBehaviour
{
    [SerializeField] private MeshRenderer meshRenderer;
    public float Length => meshRenderer.bounds.size.z;

    // POOL Methods

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

    public class Pool : MonoMemoryPool<Vector3, GroundTile>
    {
        protected override void OnCreated(GroundTile item)
        {
            base.OnCreated(item);
            item.SetPool(this);
        }

        protected override void OnDespawned(GroundTile item)
        {
            base.OnDespawned(item);
        }

        protected override void OnDestroyed(GroundTile item)
        {
            base.OnDestroyed(item);
        }

        protected override void OnSpawned(GroundTile item)
        {
            base.OnSpawned(item);
        }

        protected override void Reinitialize(Vector3 position, GroundTile item)
        {
            base.Reinitialize(position, item);
            item.SetPosition(position);
        }
    }
}
