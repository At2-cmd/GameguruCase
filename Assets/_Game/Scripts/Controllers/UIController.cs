using Sirenix.OdinInspector;
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
        DisableAllPopups();
        tapToStart.Initialize();
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
