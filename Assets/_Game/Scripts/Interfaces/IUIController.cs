using UnityEngine;
public interface IUIController
{
    void ShowLevelCompletedPopup();
    void ShowLevelFailedPopup();
    void DisableAllPopups();
    void UpdateMatchCounterText(int matchCount);

}
