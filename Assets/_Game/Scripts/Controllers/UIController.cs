using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

public class UIController : MonoBehaviour, IInitializable, IUIController
{
    [SerializeField] private PopupBase levelCompletedPopup;
    [SerializeField] private PopupBase levelFailedPopup;
    public void Initialize()
    {
        DisableAllPopups();
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
