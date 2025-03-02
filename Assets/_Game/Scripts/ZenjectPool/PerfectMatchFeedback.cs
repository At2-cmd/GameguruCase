using UnityEngine;
using Zenject;
using DG.Tweening;

public class PerfectMatchFeedback : MonoBehaviour
{
    [SerializeField] private Renderer meshRenderer;
    [SerializeField] private float fadeOutDuration;
    private Pool _pool;
    private Color _defaultColor;
    private Color _targetColor;


    private void Initialize()
    {
        _defaultColor = meshRenderer.material.color;
        _targetColor = _defaultColor;
        _targetColor.a = 0;
    }

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

    private void StartAnimation()
    {
        meshRenderer.material.color = _defaultColor;
        meshRenderer.material.DOColor(_targetColor, fadeOutDuration);
    }

    public class Pool : MonoMemoryPool<Vector3, PerfectMatchFeedback>
    {
        protected override void OnCreated(PerfectMatchFeedback item)
        {
            base.OnCreated(item);
            item.SetPool(this);
            item.Initialize();
        }

        protected override void Reinitialize(Vector3 position, PerfectMatchFeedback item)
        {
            base.Reinitialize(position, item);
            item.SetPosition(position);
            item.StartAnimation();
        }
    }
}
