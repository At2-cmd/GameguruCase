using System;
using UnityEngine;
using Zenject;

public class CubeSlicer : MonoBehaviour
{
    [Inject] AudioSystem _audioSystem;
    [Inject] GroundTile.Pool groundTilePool;
    [Inject] PerfectMatchFeedback.Pool perfectMatchFeedbackPool;
    private Transform frontCube;
    private Transform backCube;
    private const float PerfectMatchThreshold = 0.1f;

    public Transform BackSideCube => backCube;
    public Transform ForwardSideCube => frontCube;

    public bool PerformSliceOperation()
    {
        float frontXMin, frontXMax, backXMin, backXMax;
        CalculateBounds(out frontXMin, out frontXMax, out backXMin, out backXMax);
        return SliceFrontCube(frontXMin, frontXMax, backXMin, backXMax);
    }

    private void CalculateBounds(out float frontXMin, out float frontXMax, out float backXMin, out float backXMax)
    {
        frontXMin = frontCube.position.x - frontCube.localScale.x / 2;
        frontXMax = frontCube.position.x + frontCube.localScale.x / 2;
        backXMin = backCube.position.x - backCube.localScale.x / 2;
        backXMax = backCube.position.x + backCube.localScale.x / 2;
    }

    private bool SliceFrontCube(float frontXMin, float frontXMax, float backXMin, float backXMax)
    {
        float overlapStart = Mathf.Max(frontXMin, backXMin);
        float overlapEnd = Mathf.Min(frontXMax, backXMax);
        float overlapSize = overlapEnd - overlapStart;
        float excessSize = Mathf.Abs(frontXMax - backXMax) > 0.01f ? Mathf.Abs(frontXMax - backXMax) : Mathf.Abs(frontXMin - backXMin);

        if (excessSize <= PerfectMatchThreshold)
        {
            HandlePerfectMatch();
            return true;
        }

        _audioSystem.GetAudioLibrary().NoteSound.SoundIndex = 0;
        _audioSystem.Play(_audioSystem.GetAudioLibrary().SliceSound);

        if (overlapSize > 0)
        {
            AdjustFrontCube(overlapStart, overlapSize);
            CreateExcessPart(frontXMin, frontXMax, backXMin, backXMax);
            return true;
        }
        else
        {
            HandleNoOverlap(frontXMin, frontXMax);
            return false;
        }
    }

    private void HandlePerfectMatch()
    {
        _audioSystem.Play(_audioSystem.GetAudioLibrary().NoteSound);
        _audioSystem.GetAudioLibrary().NoteSound.SoundIndex++;
        frontCube.position = new Vector3(backCube.position.x, frontCube.position.y, frontCube.position.z);
        frontCube.localScale = new Vector3(backCube.localScale.x, frontCube.localScale.y, frontCube.localScale.z);
        perfectMatchFeedbackPool.Spawn(frontCube.position).transform.localScale = frontCube.localScale;
    }

    private void AdjustFrontCube(float overlapStart, float overlapSize)
    {
        frontCube.position = new Vector3(overlapStart + overlapSize / 2, frontCube.position.y, frontCube.position.z);
        frontCube.localScale = new Vector3(overlapSize, frontCube.localScale.y, frontCube.localScale.z);
    }

    private void CreateExcessPart(float frontXMin, float frontXMax, float backXMin, float backXMax)
    {
        float excessSize = Mathf.Abs(frontXMax - backXMax) > 0.01f ? Mathf.Abs(frontXMax - backXMax) : Mathf.Abs(frontXMin - backXMin);
        if (excessSize > 0.01f)
        {
            float excessX = (frontXMax > backXMax) ? frontXMax - excessSize / 2 : frontXMin + excessSize / 2;
            SpawnFallingExcessPart(excessX, excessSize);
        }
    }

    private void SpawnFallingExcessPart(float positionX, float sizeX)
    {
        GroundTile excessTile = groundTilePool.Spawn(new Vector3(positionX, frontCube.position.y, frontCube.position.z));
        excessTile.transform.localScale = new Vector3(sizeX, frontCube.localScale.y, frontCube.localScale.z);
        excessTile.SetRigidBodyKinematicStatus(false);
    }

    private void HandleNoOverlap(float frontXMin, float frontXMax)
    {
        float slicedSize = Mathf.Abs(frontXMax - frontXMin);
        SpawnFallingExcessPart((frontXMin + frontXMax) / 2, slicedSize);
        frontCube.localScale = Vector3.zero;
    }

    public void SetBackSideCube(GroundTile backSideCube)
    {
        backCube = backSideCube.transform;
    }

    public void SetForwardSideCube(GroundTile forwardSideCube)
    {
        frontCube = forwardSideCube.transform;
    }

    public void ResetSlicer()
    {
        backCube = null;
        frontCube = null;
    }
}
