using System;
using UnityEngine;
using Zenject;

public class TouchInputController : MonoBehaviour, IInitializable, ITouchInputController
{
    [Inject] IGameManager _gameManager;
    [Inject] AudioSystem _audioSystem;
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
                _gameManager.OnGameFailed();
                CanUserGiveInput = false;
                return;
            }
            _audioSystem.Play(_audioSystem.GetAudioLibrary().NoteSound);

            _groundTileController.CurrentGroundTile.SetYoyoMovementStatus(false);
            _playerController.MovePlayerToPosition(_groundTileController.CurrentGroundTile.transform.position,() => _playerController.PlayAnim(AnimationState.Idle));

            _sliceController.AssignBackSideCube(_groundTileController.CurrentGroundTile);
            _sliceController.AssignForwardSideCube(_groundTileController.GenerateGroundTile());
        }
    }
}
