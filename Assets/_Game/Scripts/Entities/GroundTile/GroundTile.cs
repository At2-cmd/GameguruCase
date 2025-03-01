using UnityEngine;
using Zenject;

public class GroundTile : MonoBehaviour
{
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private Rigidbody rigidbody;
    [SerializeField] private TileYoyoMovement tileYoyoMovement;
    private Vector3 _defaultScale;
    private Color _defaultColor;
    private MaterialPropertyBlock _materialPropertyBlock;
    private static readonly int ColorProperty = Shader.PropertyToID("_BaseColor");
    public float Length => meshRenderer.bounds.size.z;
    public float Width => meshRenderer.bounds.size.x;
    private void Initialize()
    {
        _defaultColor = meshRenderer.material.color;
        _defaultScale = transform.localScale;
        _materialPropertyBlock = new MaterialPropertyBlock();
    }
    public void SetColor(Color newColor)
    {
        meshRenderer.GetPropertyBlock(_materialPropertyBlock);
        _materialPropertyBlock.SetColor(ColorProperty, newColor);
        meshRenderer.SetPropertyBlock(_materialPropertyBlock);
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
        SetColor(_defaultColor);
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
        if (!gameObject.activeSelf) return;
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
