using UnityEngine;
using Zenject;

public class GridController : MonoBehaviour, IInitializable
{
    [SerializeField] private GridAreaEntity gridAreaEntity;

    public void Initialize()
    {
        gridAreaEntity.InitializeGridCells();
    }
}
