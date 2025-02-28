using UnityEngine;
using Zenject;

public class TouchInputController : MonoBehaviour, IInitializable
{
    [Inject] private IGroundTileController _groundTileController;
    public void Initialize()
    {

    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _groundTileController.GenerateGroundTile();
        }
    }
}
