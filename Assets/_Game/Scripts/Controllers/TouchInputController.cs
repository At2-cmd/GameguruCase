using System;
using UnityEngine;
using Zenject;

public class TouchInputController : MonoBehaviour, IInitializable, ITouchInputController
{
    [Inject] IGameManager _gameManager;
    [Inject] private IGroundTileController _groundTileController;
    [Inject] private ISlicerController _sliceController;
    [Inject] private IPlayerController _playerController;

    public bool CanUserGiveInput { get; set; }

    public void Initialize() {}

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && CanUserGiveInput)
        {
            bool canPerformSlice = _sliceController.Slice();
            if (!canPerformSlice)
            {
                CanUserGiveInput = false;
                _gameManager.OnGameFailed();
                return;
            }
            _groundTileController.CurrentGroundTile.SetYoyoMovementStatus(false);
            _playerController.MovePlayerToLastTile(_groundTileController.CurrentGroundTile.transform.position);
            _sliceController.AssignBackSideCube(_groundTileController.CurrentGroundTile);
            _sliceController.AssignForwardSideCube(_groundTileController.GenerateGroundTile());
        }
    }
}
