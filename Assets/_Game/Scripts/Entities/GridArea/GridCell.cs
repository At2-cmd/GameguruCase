using UnityEngine;

public class GridCell : MonoBehaviour
{
    [SerializeField] private GameObject xSignObject;
    [SerializeField] private Renderer renderer;
    [SerializeField] private Material hoverMaterial;
    [SerializeField] private Material selectedMaterial;
    private Material _originalMaterial;

    private int xPositionIndex;
    private int yPositionIndex;
    private GridAreaEntity gridArea;

    
    private MaterialPropertyBlock _propBlock;

    public void Initialize(GridAreaEntity parentGrid)
    {
        gridArea = parentGrid;
        xSignObject.SetActive(false);
        _originalMaterial = renderer.material;
    }

    public void AssignValues(int x, int y)
    {
        xPositionIndex = x;
        yPositionIndex = y;
    }

    private void OnMouseEnter()
    {
        if (HasXSign()) return;
        renderer.material = hoverMaterial;
    }

    private void OnMouseExit()
    {
        if (HasXSign()) return;
        renderer.material = _originalMaterial;
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
            renderer.material = selectedMaterial;
            gridArea.CheckForMatches(xPositionIndex, yPositionIndex);
        }
    }

    public bool HasXSign() => xSignObject.activeSelf;

    public void DeactivateCell()
    {
        xSignObject.SetActive(false);
        renderer.material = _originalMaterial;
    }
}