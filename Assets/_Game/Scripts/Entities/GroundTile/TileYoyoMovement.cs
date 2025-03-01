using UnityEngine;
using DG.Tweening;

public class TileYoyoMovement : MonoBehaviour
{
    [SerializeField] private float movementAmount;
    [SerializeField] private float duration = 1f;
    [SerializeField] private Ease easeType;
    private Tweener _yoyoTween;
    public void StartYoyoMovement()
    {
        _yoyoTween = transform.DOMoveX(transform.position.x + movementAmount, duration)
            .SetEase(easeType)
            .SetLoops(-1, LoopType.Yoyo);
    }

    public void StopYoyoMovement()
    {
        if (_yoyoTween != null && _yoyoTween.IsActive())
        {
            _yoyoTween.Kill();
        }
    }
}
