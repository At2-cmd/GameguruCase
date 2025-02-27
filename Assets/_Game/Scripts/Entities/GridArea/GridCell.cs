using UnityEngine;

public class GridCell : MonoBehaviour
{
    [SerializeField] private GameObject xSignObject;
    private int xPositionIndex;
    private int yPositionIndex;
    public void Initialize()
    {
        Debug.Log("Grid Cell Initialized!");
    }
    public void SetXSignStatus(bool value)
    {
        xSignObject.SetActive(value);
    }

    public void AssignValues(int x, int y)
    {
        xPositionIndex = x;
        yPositionIndex = y;
    }
}
