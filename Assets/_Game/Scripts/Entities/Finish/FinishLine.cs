using System;
using UnityEngine;
using Zenject;

public class FinishLine : MonoBehaviour
{
    [Inject] private IGameManager _gameManager;
    [Inject] private ITouchInputController _touchInputController;
    private bool _isFinishLineTriggered;

    private void OnTriggerEnter(Collider other)
    {
        if(_isFinishLineTriggered) return;
        if (other.TryGetComponent(out PlayerMovement playerMovement))
        {
            playerMovement.Move(transform.position, () =>
            {
                _isFinishLineTriggered = true;
                _touchInputController.CanUserGiveInput = false;
                _gameManager.OnGameSuccessed();
            });
        }
    }
    public void ResetFinishLine()
    {
        _isFinishLineTriggered = false;
    }
}
