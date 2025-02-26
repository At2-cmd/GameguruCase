using Zenject;
public class LevelFailedPopupView : PopupBase
{
    public override void Initialize()
    {
        base.Initialize();
    }
    public void OnRetryLevelButtonClicked()
    {
        SetPopupActiveness(false);
        BlackScreen.Open(() => 
        {
            EventController.Instance.RaiseLevelProceedButtonClicked();
            BlackScreen.Close();
        });
    }
}
