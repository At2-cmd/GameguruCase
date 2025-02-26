using _GameData.Scripts.Managers;

public class LevelCompletedPopupView : PopupBase
{
    public override void Initialize()
    {
        base.Initialize();
    }
    public void OnNextLevelButtonClicked()
    {
        SaveManager.SetInt(SaveManager.levelndex, SaveManager.GetInt(SaveManager.levelndex) + 1);
        SetPopupActiveness(false);
        BlackScreen.Open(() =>
        {
            EventController.Instance.RaiseLevelProceedButtonClicked();
            BlackScreen.Close();
        });
    }
}