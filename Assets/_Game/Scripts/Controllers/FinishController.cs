using System;
using UnityEngine;
using Zenject;

public class FinishController : MonoBehaviour, IInitializable
{
    [SerializeField] private FinishLine finishLine;

    public void Initialize()
    {
        Subscribe();
    }

    private void OnDestroy()
    {
        Unsubscribe();
    }

    private void Subscribe()
    {
        EventController.Instance.OnLevelProceedButtonClicked += OnLevelProceedButtonClickedHandler;
    }
    private void Unsubscribe()
    {
        EventController.Instance.OnLevelProceedButtonClicked -= OnLevelProceedButtonClickedHandler;
    }
    private void OnLevelProceedButtonClickedHandler()
    {
        finishLine.ResetFinishLine();
    }
}
