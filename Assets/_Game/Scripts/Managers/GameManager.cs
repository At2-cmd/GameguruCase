using UnityEngine;
using Zenject;

public class GameManager : MonoBehaviour, IInitializable, IGameManager
{
    [Inject] IUIController _uiController;
    [Inject] IPlayerController _playerController;
    public void Initialize()
    {
        Debug.Log("GameManager initialized");
    }

    public void OnGameFailed()
    {
        _uiController.ShowLevelFailedPopup();
        _playerController.PlayAnim(AnimationState.Fall);
    }

    public void OnGameSuccessed()
    {
        _uiController.ShowLevelCompletedPopup();
        _playerController.PlayAnim(AnimationState.Dance);
    }
}
