using UnityEngine;
using Zenject;

public class TouchInputController : MonoBehaviour, IInitializable
{
    [Inject] private IGroundTileController _groundTileController;
    [Inject] private ISlicerController _sliceController;
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
            _sliceController.AssignBackSideCube(_groundTileController.CurrentGroundTile);
            _sliceController.AssignForwardSideCube(_groundTileController.GenerateGroundTile());
        }
    }
}
