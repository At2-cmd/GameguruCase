using System;
using UnityEngine;
using Zenject;

public class EventController : MonoBehaviour, IInitializable
{
    public static EventController Instance { get; private set; }
    public void Initialize()
    {
        Instance = this;
    }

    public event Action OnLevelGenerated;
    public void RaiseLevelGenerated() => OnLevelGenerated?.Invoke();

    public event Action OnGameStarted;
    public void RaiseGameStarted() => OnGameStarted?.Invoke();
    
    public event Action OnGameSuccessed;
    public void RaiseGameSuccessed() => OnGameSuccessed?.Invoke();

    public event Action OnGameFailed;
    public void RaiseGameFailed() => OnGameFailed?.Invoke();

    public event Action OnLevelProceedButtonClicked;
    public void RaiseLevelProceedButtonClicked() => OnLevelProceedButtonClicked?.Invoke();
}
