using UnityEngine;
using Zenject;

public class GridCell : MonoBehaviour
{
    [Inject] AudioSystem _audioSystem;
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
            _audioSystem.Play(_audioSystem.GetAudioLibrary().PinSound);
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

    // POOL METHODS

    private Pool _pool;

    public void Despawn()
    {
        DeactivateCell();
        _pool.Despawn(this);
    }

    private void SetPool(Pool pool)
    {
        _pool = pool;
    }

    private void SetPosition(Vector3 position)
    {
        transform.position = position;
    }

    public class Pool : MonoMemoryPool<Vector3, GridCell>
    {
        protected override void OnCreated(GridCell item)
        {
            base.OnCreated(item);
            item.SetPool(this);
        }

        protected override void Reinitialize(Vector3 position, GridCell item)
        {
            base.Reinitialize(position, item);
            item.SetPosition(position);
        }
    }
}