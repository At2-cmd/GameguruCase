using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Zenject;

public class GridAreaEntity : MonoBehaviour
{
    [Inject] private ICameraController _cameraController;
    [Inject] private IUIController _uiCotroller;
    [Inject] private StarExplosionParticle.Pool _starParticlePool;
    [Inject] private GridCell.Pool _gridCellsPool;
    [SerializeField] private int rows = 4;
    [SerializeField] private int columns = 4;
    [SerializeField] private float cellSpacing = 1.0f;
    private GridCell[,] _gridCells;
    private List<GridCell> _matchedCells = new List<GridCell>();
    private int _matchCount;

    public void Initialize()
    {
        GenerateGrid(rows, columns);
    }

    public void GenerateGrid(int xDimension , int yDimension)
    {
        ClearGrid();

        _gridCells = new GridCell[yDimension, xDimension];
        Vector3 startPosition = new Vector3(-(yDimension - 1) * 0.5f * cellSpacing, 0, -(xDimension - 1) * 0.5f * cellSpacing);

        for (int x = 0; x < yDimension; x++)
        {
            for (int y = 0; y < xDimension; y++)
            {
                Vector3 position = startPosition + new Vector3(x * cellSpacing, 0, y * cellSpacing);
                GridCell gridCell = _gridCellsPool.Spawn(position);
                gridCell.AssignValues(x, y);
                gridCell.Initialize(this);
                _gridCells[x, y] = gridCell;
            }
        }
        _cameraController.AdjustCameraView(yDimension >= xDimension ? yDimension : xDimension);
    }

    public void ClearGrid()
    {
        if (_gridCells == null) return;
        foreach (GridCell cell in _gridCells)
        {
            cell.Despawn();
        }
        _matchCount = 0;
        _uiCotroller.UpdateMatchCounterText(_matchCount);
    }

    public void CheckForMatches(int x, int y)
    {
        _matchedCells.Clear();
        FindConnectedCells(x, y);

        if (_matchedCells.Count >= 3)
        {
            _matchCount++;
            _uiCotroller.UpdateMatchCounterText(_matchCount);
            foreach (GridCell cell in _matchedCells)
            {
                _starParticlePool.Spawn(cell.transform.position + Vector3.up);
                cell.DeactivateCell();
            }
        }
    }

    private void FindConnectedCells(int x, int y)
    {
        if (x < 0 || x >= _gridCells.GetLength(0) || y < 0 || y >= _gridCells.GetLength(1))
            return;

        GridCell cell = _gridCells[x, y];
        if (!cell.HasXSign() || _matchedCells.Contains(cell)) return;

        _matchedCells.Add(cell);
        FindConnectedCells(x + 1, y); // Right
        FindConnectedCells(x - 1, y); // Left
        FindConnectedCells(x, y + 1); // Up
        FindConnectedCells(x, y - 1); // Down
    }

}
