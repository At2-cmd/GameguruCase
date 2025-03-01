using UnityEngine;
using Zenject;

public class GroundTile : MonoBehaviour
{
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private Rigidbody rigidbody;
    [SerializeField] private TileYoyoMovement tileYoyoMovement;
    public float Length => meshRenderer.bounds.size.z;
    public float Width => meshRenderer.bounds.size.x;

    private Vector3 _defaultScale;
    private void Initialize()
    {
        _defaultScale = transform.localScale;
    }

    public void SetRigidBodyKinematicStatus(bool isKinematic)
    {
        rigidbody.isKinematic = isKinematic;
    }
    private void OnDespawned()
    {
        SetRigidBodyKinematicStatus(true);
        SetPosition(Vector3.zero);
        transform.rotation = Quaternion.identity;
        transform.localScale = _defaultScale;
        SetYoyoMovementStatus(false);
    }

    public void SetYoyoMovementStatus(bool value)
    {
        if (value)
        {
            tileYoyoMovement.StartYoyoMovement();
        }
        else
        {
            tileYoyoMovement.StopYoyoMovement();
        }
    }

    public void SetYoyoTarget(float targetX) => tileYoyoMovement.SetYoyoTarget(targetX);

    // POOL Methods

    private Pool _pool;
    public void Despawn()
    {
        OnDespawned();
        _pool.Despawn(this);
    }

    private void SetPool(Pool pool)
    {
        _pool = pool;
    }

    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }

    public class Pool : MonoMemoryPool<Vector3, GroundTile>
    {
        protected override void OnCreated(GroundTile item)
        {
            base.OnCreated(item);
            item.SetPool(this);
            item.Initialize();
        }

        protected override void Reinitialize(Vector3 position, GroundTile item)
        {
            base.Reinitialize(position, item);
            item.SetPosition(position);
        }
    }
}
