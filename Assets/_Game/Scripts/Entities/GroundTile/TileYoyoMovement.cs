using UnityEngine;
using DG.Tweening;
using System;

public class TileYoyoMovement : MonoBehaviour
{
    [SerializeField] private float duration = 1f;
    [SerializeField] private Ease easeType;
    private float _yoyoTargetX;
    private Tweener _yoyoTween;
    public void StartYoyoMovement()
    {
        _yoyoTween = transform.DOMoveX(_yoyoTargetX, duration)
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

    public void SetYoyoTarget(float targetX)
    {
        _yoyoTargetX = targetX;
    }
}
