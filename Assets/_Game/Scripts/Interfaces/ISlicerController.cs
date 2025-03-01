using UnityEngine;

public interface ISlicerController
{
    bool Slice();
    void ResetSlicer();
    void AssignBackSideCube(GroundTile backSideCube);
    void AssignForwardSideCube(GroundTile forwardSideCube);
    Transform CurrentBackSideCube { get; }
    Transform CurrentForwardSideCube { get; }
}
