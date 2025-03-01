using UnityEngine;
using Zenject;

public class TouchInputController : MonoBehaviour, IInitializable
{
    [Inject] private IGroundTileController _groundTileController;
    [Inject] private ISlicerController _sliceController;
    [Inject] private IPlayerController _playerController;
    public void Initialize()
    {
        _sliceController.AssignForwardSideCube(_groundTileController.GenerateGroundTile());
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _sliceController.Slice();
            _groundTileController.CurrentGroundTile.SetYoyoMovementStatus(false);
            _playerController.MovePlayerToLastTile(_groundTileController.CurrentGroundTile.transform.position);
            _sliceController.AssignBackSideCube(_groundTileController.CurrentGroundTile);
            _sliceController.AssignForwardSideCube(_groundTileController.GenerateGroundTile());
        }
    }
}
