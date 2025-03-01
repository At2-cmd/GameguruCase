using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerController : MonoBehaviour, IInitializable, IPlayerController
{
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerAnimation playerAnimation;
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
        PlayAnim(AnimationState.Idle);
        transform.position = Vector3.zero;
    }

    public void MovePlayerToLastTile(Vector3 lastTilePosition)
    {
        PlayAnim(AnimationState.Run);
        playerMovement.Move(lastTilePosition, () => PlayAnim(AnimationState.Idle));
    }

    public void PlayAnim(AnimationState animState)
    {
        playerAnimation.PlayAnim(animState);
    }
}
