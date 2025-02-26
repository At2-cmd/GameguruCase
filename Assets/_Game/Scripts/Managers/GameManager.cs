using UnityEngine;
using Zenject;

public class GameManager : MonoBehaviour, IInitializable, IGameManager
{
    public void Initialize()
    {
        Debug.Log("GameManager initialized");
    }
}
