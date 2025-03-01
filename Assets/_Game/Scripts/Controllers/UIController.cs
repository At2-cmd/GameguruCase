using Sirenix.OdinInspector;
using System;
using TMPro;
using UnityEngine;
using Zenject;

public class UIController : MonoBehaviour, IInitializable, IUIController
{
    [SerializeField] private PopupBase levelCompletedPopup;
    [SerializeField] private PopupBase levelFailedPopup;
    [SerializeField] private TapToStart tapToStart;
    public void Initialize()
    {
        Subscribe();
        DisableAllPopups();
        tapToStart.Initialize();
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
        tapToStart.gameObject.SetActive(true);
    }

    public void DisableAllPopups()
    {
        levelCompletedPopup.SetPopupActiveness(false);
        levelFailedPopup.SetPopupActiveness(false);
    }
    [Button]
    public void ShowLevelCompletedPopup()
    {
        DisableAllPopups();
        levelCompletedPopup.SetPopupActiveness(true);
        levelCompletedPopup.Initialize();
    }
    [Button]
    public void ShowLevelFailedPopup()
    {
        DisableAllPopups();
        levelFailedPopup.SetPopupActiveness(true);
        levelFailedPopup.Initialize();
    }
}
