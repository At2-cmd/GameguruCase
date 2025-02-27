using UnityEngine;

public class GridCell : MonoBehaviour
{
    [SerializeField] private GameObject xSignObject;
    private int xPositionIndex;
    private int yPositionIndex;
    private GridAreaEntity gridArea;

    public void Initialize(GridAreaEntity parentGrid)
    {
        gridArea = parentGrid;
        xSignObject.SetActive(false);
    }

    public void AssignValues(int x, int y)
    {
        xPositionIndex = x;
        yPositionIndex = y;
    }

    private void OnMouseDown()
    {
        if (HasXSign()) return;
        ToggleXSign();
    }

    private void ToggleXSign()
    {
        bool newState = !xSignObject.activeSelf;
        xSignObject.SetActive(newState);

        if (newState)
        {
            gridArea.CheckForMatches(xPositionIndex, yPositionIndex);
        }
    }

    public bool HasXSign() => xSignObject.activeSelf;

    public void DeactivateCell()
    {
        xSignObject.SetActive(false);
    }
}
