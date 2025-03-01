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
        EventController.Instance.OnGameStarted += OnGameStartedHandler;
    }

    private void Unsubscribe()
    {
        EventController.Instance.OnGameStarted -= OnGameStartedHandler;
    }

    private void OnGameStartedHandler()
    {
        playerAnimation.PlayAnim(AnimationState.Run);
    }

    public void MovePlayerToLastTile(Vector3 lastTilePosition)
    {
        playerAnimation.PlayAnim(AnimationState.Run);
        playerMovement.Move(lastTilePosition, () => playerAnimation.PlayAnim(AnimationState.Idle));
    }
}
