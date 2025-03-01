using _GameData.Scripts.Managers;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class LevelLoaderController : MonoBehaviour, IInitializable, ILevelLoaderController, ILevelDataProvider
{
    [Inject] DiContainer _container;
    [SerializeField] private List<LevelScriptableObject> levelDatas;
    private GameObject _currentLevelPrefab;
    private int _loopedLevelIndex => CalculateLoopedIndex(SaveManager.GetInt(SaveManager.levelndex));

    public void Initialize()
    {
        Subscribe();
        GenerateCorrespondingLevelPrefab();
    }

    private void OnDestroy()
    {
        Unsubscribe();
    }
    private void Subscribe()
    {
        EventController.Instance.OnLevelProceedButtonClicked += OnLevelProceedButtonClickedHandler;
    }

    private void Unsubscribe()
    {
        EventController.Instance.OnLevelProceedButtonClicked -= OnLevelProceedButtonClickedHandler;
    }

    private void OnLevelProceedButtonClickedHandler()
    {
        GenerateCorrespondingLevelPrefab();
    }
    private void GenerateCorrespondingLevelPrefab()
    {
        if (_currentLevelPrefab) Destroy(_currentLevelPrefab);
        _currentLevelPrefab = _container.InstantiatePrefab(levelDatas[_loopedLevelIndex].LevelPrefab);
        EventController.Instance.RaiseLevelGenerated();
    }
    private int CalculateLoopedIndex(int levelIndex)
    {
        return levelIndex % (levelDatas.Count);
    }

    public TileColorData GetTileColorsData()
    {
        return levelDatas[_loopedLevelIndex].TileColorsDataList;
    }
}
