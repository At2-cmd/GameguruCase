using UnityEngine;

public interface ISlicerController
{
    void Slice();
    void AssignBackSideCube(GroundTile backSideCube);
    void AssignForwardSideCube(GroundTile forwardSideCube);
    Transform CurrentBackSideCube { get; }
    Transform CurrentForwardSideCube { get; }
}
