using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerController : MonoBehaviour, IInitializable, IPlayerController
{
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerAnimation playerAnimation;
    [SerializeField] private Transform camFollowReferenceTransform;

    public Transform PlayerTransform => playerMovement.transform;

    public void Initialize()
    {
        playerMovement.Initialize();
        playerAnimation.Initialize();
        Subscribe();
    }

    private void OnDestroy()
    {
        Unsubscribe();
    }
    private void Subscribe()
    {
        EventController.Instance.OnLevelProceedButtonClicked += OnLevelProceedButtonClickedHandler;
    }

    private void Unsubscribe()
    {
        EventController.Instance.OnLevelProceedButtonClicked -= OnLevelProceedButtonClickedHandler;
    }

    private void OnLevelProceedButtonClickedHandler()
    {
        playerMovement.transform.DOKill();
        playerMovement.transform.position = Vector3.zero;
        PlayAnim(AnimationState.Idle);
    }

    public void MovePlayerToPosition(Vector3 lastTilePosition, Action onCompleteCallBack = null)
    {
        PlayAnim(AnimationState.Run);
        playerMovement.Move(lastTilePosition, () => onCompleteCallBack?.Invoke());
    }

    public void PlayAnim(AnimationState animState)
    {
        playerAnimation.PlayAnim(animState);
    }
}
