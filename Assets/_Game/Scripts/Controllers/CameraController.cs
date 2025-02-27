using UnityEngine;

public class CameraController : MonoBehaviour, ICameraController
{
    [SerializeField] private Camera gameplayCamera;
    private const float _cameraSizeOffset = 0.5f;
    public void AdjustCameraView(int size)
    {
        gameplayCamera.orthographicSize = size + _cameraSizeOffset;
    }
}
