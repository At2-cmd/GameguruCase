using DG.Tweening;
using UnityEngine;
using Zenject;

public class GameManager : MonoBehaviour, IInitializable, IGameManager
{
    [Inject] IUIController _uiController;
    [Inject] AudioSystem _audioSystem;
    [Inject] IPlayerController _playerController;
    public void Initialize()
    {
        Debug.Log("GameManager initialized");
    }

    public void OnGameFailed()
    {
        DOTween.KillAll();
        DOVirtual.DelayedCall(2, () => 
        {
            _audioSystem.Play(_audioSystem.GetAudioLibrary().FailSound);
            _uiController.ShowLevelFailedPopup();
        });
        _playerController.MovePlayerToPosition(_playerController.PlayerTransform.position + Vector3.forward * 2, () =>
        {
            _playerController.MovePlayerToPosition(_playerController.PlayerTransform.position + Vector3.down * 20);
            _playerController.PlayAnim(AnimationState.Fall);
        });
    }

    public void OnGameSuccessed()
    {
        DOTween.KillAll();
        _audioSystem.Play(_audioSystem.GetAudioLibrary().SuccessSound);
        DOVirtual.DelayedCall(2, _uiController.ShowLevelCompletedPopup);
        _playerController.PlayAnim(AnimationState.Dance);
        _playerController.RotateCamReferencePoint();
    }
}
