using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "LevelData")]
public class LevelScriptableObject : ScriptableObject
{
    [SerializeField] private GameObject levelPrefab;
    public GameObject LevelPrefab => levelPrefab;
}
