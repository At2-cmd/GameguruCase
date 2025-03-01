using UnityEngine;

public class SliceController : MonoBehaviour, ISlicerController
{
    [SerializeField] private CubeSlicer cubeSlicer;

    public Transform CurrentBackSideCube => cubeSlicer.BackSideCube;

    public Transform CurrentForwardSideCube => cubeSlicer.ForwardSideCube;

    public void AssignBackSideCube(GroundTile backSideCube)
    {
        cubeSlicer.SetBackSideCube(backSideCube);
    }

    public void AssignForwardSideCube(GroundTile backSideCube)
    {
        cubeSlicer.SetForwardSideCube(backSideCube);
    }

    public void Slice()
    {
        cubeSlicer.PerformSliceOperation();
    }
}
