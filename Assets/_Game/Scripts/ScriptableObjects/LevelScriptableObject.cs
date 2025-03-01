using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "LevelData")]
public class LevelScriptableObject : ScriptableObject
{
    [SerializeField] private GameObject levelPrefab;

    [SerializeField] private TileColorData tileColorsDataList;
    public GameObject LevelPrefab => levelPrefab;
    public TileColorData TileColorsDataList => tileColorsDataList;
}
