using UnityEngine;
using Zenject;

public class GridController : MonoBehaviour, IInitializable, IGridController
{
    [SerializeField] private GridAreaEntity gridAreaEntity;


    public void Initialize()
    {
        gridAreaEntity.Initialize();
    }
    public void GenerateGrid(int xDimension, int yDimension)
    {
        gridAreaEntity.GenerateGrid(xDimension, yDimension);
    }
}
