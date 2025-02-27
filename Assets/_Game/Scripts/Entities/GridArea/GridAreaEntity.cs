using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GridAreaEntity : MonoBehaviour
{
    [SerializeField] private GridCell gridCellPrefab;
    [SerializeField] private int rows = 4;
    [SerializeField] private int columns = 4;
    [SerializeField] private float cellSpacing = 1.0f;
    private GridCell[,] gridCells;

    public void InitializeGridCells()
    {
        GenerateGrid();
        foreach (GridCell cell in gridCells)
        {
            cell.Initialize(this);
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

#if UNITY_EDITOR
                GameObject cell = (GameObject)PrefabUtility.InstantiatePrefab(gridCellPrefab.gameObject, transform);
#else
                GameObject cell = Instantiate(gridCellPrefab.gameObject, transform);
#endif
                cell.transform.position = position;
                GridCell gridCell = cell.GetComponent<GridCell>();
                gridCell.AssignValues(x, y);
                gridCells[x, y] = gridCell;
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

    public void CheckForMatches(int x, int y)
    {
        List<GridCell> matchedCells = new List<GridCell>();
        FindConnectedCells(x, y, matchedCells);

        if (matchedCells.Count >= 3)
        {
            foreach (GridCell cell in matchedCells)
            {
                cell.DeactivateCell();
            }
        }
    }

    private void FindConnectedCells(int x, int y, List<GridCell> matchedCells)
    {
        if (x < 0 || x >= columns || y < 0 || y >= rows) return;

        GridCell cell = gridCells[x, y];
        if (!cell.HasXSign() || matchedCells.Contains(cell)) return;

        matchedCells.Add(cell);

        FindConnectedCells(x + 1, y, matchedCells); // Right
        FindConnectedCells(x - 1, y, matchedCells); // Left
        FindConnectedCells(x, y + 1, matchedCells); // Up
        FindConnectedCells(x, y - 1, matchedCells); // Down
    }
}
