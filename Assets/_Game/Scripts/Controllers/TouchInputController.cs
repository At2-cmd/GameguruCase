using UnityEngine;
using Zenject;

public class TouchInputController : MonoBehaviour, IInitializable, ITouchInputController
{
    [Inject] private IGroundTileController _groundTileController;
    [Inject] private ISlicerController _sliceController;
    [Inject] private IPlayerController _playerController;

    public bool CanUserGiveInput { get; set; }

    public void Initialize() 
    {
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && CanUserGiveInput)
        {
            _sliceController.Slice();
            _groundTileController.CurrentGroundTile.SetYoyoMovementStatus(false);
            _playerController.MovePlayerToLastTile(_groundTileController.CurrentGroundTile.transform.position);
            _sliceController.AssignBackSideCube(_groundTileController.CurrentGroundTile);
            _sliceController.AssignForwardSideCube(_groundTileController.GenerateGroundTile());
        }
    }
}
