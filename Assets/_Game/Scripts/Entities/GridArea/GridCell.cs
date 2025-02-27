using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCell : MonoBehaviour
{
    private Renderer cellRenderer;
    private Color defaultColor;
    public void Initialize()
    {
        Debug.Log("Grid Cell Initialized!");
        cellRenderer = GetComponent<Renderer>();
        defaultColor = cellRenderer.material.color;
    }
    public void HighlightCell(Color color)
    {
        cellRenderer.material.color = color;
    }
    public void ResetCellColor()
    {
        cellRenderer.material.color = defaultColor;
    }
}
