using DG.Tweening;
using System;
using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveDuration;
    private Tweener _moveTween;
    public void Initialize()
    {

    }
    public void Move(Vector3 lastTilePosition, Action onMovementCompleted = null)
    {
        _moveTween?.Kill();
        _moveTween = transform.DOMove(lastTilePosition, moveDuration).SetEase(Ease.Linear).OnComplete(() => 
        {
            onMovementCompleted?.Invoke();
        });
    }
}
