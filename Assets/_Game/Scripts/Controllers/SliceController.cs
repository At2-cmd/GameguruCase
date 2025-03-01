using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliceController : MonoBehaviour, ISlicerController
{
    [SerializeField] private CubeSlicer cubeSlicer;

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
