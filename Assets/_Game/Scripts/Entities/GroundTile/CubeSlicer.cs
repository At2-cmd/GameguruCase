using System;
using UnityEngine;
using Zenject;

public class CubeSlicer : MonoBehaviour
{
    [Inject] GroundTile.Pool groundTilePool;
    private Transform frontCube;
    private Transform backCube;


    public void PerformSliceOperation()
    {
        float frontXMin, frontXMax, backXMin, backXMax;
        CalculateBounds(out frontXMin, out frontXMax, out backXMin, out backXMax);

        if (frontXMin < backXMin || frontXMax > backXMax)
        {
            SliceFrontCube(frontXMin, frontXMax, backXMin, backXMax);
        }
    }

    void CalculateBounds(out float frontXMin, out float frontXMax, out float backXMin, out float backXMax)
    {
        frontXMin = frontCube.position.x - frontCube.localScale.x / 2;
        frontXMax = frontCube.position.x + frontCube.localScale.x / 2;
        backXMin = backCube.position.x - backCube.localScale.x / 2;
        backXMax = backCube.position.x + backCube.localScale.x / 2;
    }

    void SliceFrontCube(float frontXMin, float frontXMax, float backXMin, float backXMax)
    {
        float overlapStart = Mathf.Max(frontXMin, backXMin);
        float overlapEnd = Mathf.Min(frontXMax, backXMax);
        float overlapSize = overlapEnd - overlapStart;

        frontCube.position = new Vector3(overlapStart + overlapSize / 2, frontCube.position.y, frontCube.position.z);
        frontCube.localScale = new Vector3(overlapSize, frontCube.localScale.y, frontCube.localScale.z);

        CreateExcessPart(frontXMin, frontXMax, backXMin, backXMax, overlapSize);
    }

    void CreateExcessPart(float frontXMin, float frontXMax, float backXMin, float backXMax, float overlapSize)
    {
        float excessSize = Mathf.Abs(frontXMax - backXMax) > 0.01f ? Mathf.Abs(frontXMax - backXMax) : Mathf.Abs(frontXMin - backXMin);
        if (excessSize > 0.01f)
        {
            float excessX = (frontXMax > backXMax) ? frontXMax - excessSize / 2 : frontXMin + excessSize / 2;
            GroundTile excessTile = groundTilePool.Spawn(new Vector3(excessX, frontCube.position.y, frontCube.position.z));
            excessTile.transform.localScale = new Vector3(excessSize, frontCube.localScale.y, frontCube.localScale.z);

            excessTile.SetRigidBodyKinematicStatus(false);
        }
    }

    public void SetBackSideCube(GroundTile backSideCube)
    {
        backCube = backSideCube.transform;
    }

    public void SetForwardSideCube(GroundTile forwardSideCube)
    {
        frontCube = forwardSideCube.transform;
    }
}
