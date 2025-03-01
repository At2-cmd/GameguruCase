using System;
using UnityEngine;
using Zenject;

public class SliceController : MonoBehaviour, IInitializable, ISlicerController
{
    [Inject] IGroundTileController _groundTileController;
    [SerializeField] private CubeSlicer cubeSlicer;
    public Transform CurrentBackSideCube => cubeSlicer.BackSideCube;
    public Transform CurrentForwardSideCube => cubeSlicer.ForwardSideCube;

    public void Initialize()
    {
        Subscribe();
        AssignBackSideCube(_groundTileController.CurrentGroundTile);
    }
    private void OnDestroy()
    {
        Unsubscribe();
    }

    private void Subscribe()
    {
        EventController.Instance.OnGameStarted += OnGameStartedHandler;
        EventController.Instance.OnLevelProceedButtonClicked += OnLevelProceedButtonClickedHandler;
    }

    private void Unsubscribe()
    {
        EventController.Instance.OnGameStarted -= OnGameStartedHandler;
        EventController.Instance.OnLevelProceedButtonClicked -= OnLevelProceedButtonClickedHandler;
    }

    private void OnLevelProceedButtonClickedHandler()
    {
        AssignBackSideCube(_groundTileController.CurrentGroundTile);
    }

    private void OnGameStartedHandler()
    {
        AssignForwardSideCube(_groundTileController.GenerateGroundTile());
    }

    public void AssignBackSideCube(GroundTile backSideCube)
    {
        cubeSlicer.SetBackSideCube(backSideCube);
    }
    public void AssignForwardSideCube(GroundTile backSideCube)
    {
        cubeSlicer.SetForwardSideCube(backSideCube);
    }

    public bool Slice()
    {
        return cubeSlicer.PerformSliceOperation();
    }

    public void ResetSlicer()
    {
        cubeSlicer.ResetSlicer();
    }
}
