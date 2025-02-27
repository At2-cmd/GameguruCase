using System;
using UnityEditor;
using UnityEngine;

public class GridAreaEntity : MonoBehaviour
{
    [SerializeField] private GridCell gridCellPrefab;
    [SerializeField] private int rows = 4;
    [SerializeField] private int columns = 4;
    [SerializeField] private float cellSpacing = 1.0f;
    [SerializeField] private GridCell[,] gridCells;
    public void InitializeGridCells()
    {
        GenerateGrid();
        foreach (GridCell cell in gridCells)
        {
            cell.Initialize();
        }
    }

    public void GenerateGrid()
    {
        ClearGrid();

        if (gridCellPrefab == null)
        {
            Debug.LogError("Grid Cell Prefab is not assigned!");
            return;
        }
        gridCells = new GridCell[columns, rows];
        Vector3 startPosition = new Vector3(-(columns - 1) * 0.5f * cellSpacing, 0, -(rows - 1) * 0.5f * cellSpacing);

        for (int x = 0; x < columns; x++)
        {
            for (int y = 0; y < rows; y++)
            {
                Vector3 position = startPosition + new Vector3(x * cellSpacing, 0, y * cellSpacing);

                // Instantiate and properly parent the object
                GameObject cell = Instantiate(gridCellPrefab.gameObject, transform);
                cell.transform.position = position; 
                gridCells[x, y] = cell.GetComponent<GridCell>();
            }
        }
    }

    public void ClearGrid()
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(transform.GetChild(i).gameObject);
        }
    }
}
