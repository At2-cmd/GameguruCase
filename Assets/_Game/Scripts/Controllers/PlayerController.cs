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
        playerMovement.transform.position = Vector3.zero;
        camFollowReferenceTransform.rotation = Quaternion.identity;
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

    public void RotateCamReferencePoint()
    {
        camFollowReferenceTransform.DORotate(new Vector3(0, 360, 0), 5f, RotateMode.FastBeyond360)
                 .SetLoops(-1, LoopType.Restart)
                 .SetEase(Ease.Linear);
    }
}
