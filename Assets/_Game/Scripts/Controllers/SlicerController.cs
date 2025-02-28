using Hanzzz.MeshSlicerFree;
using System.Linq;
using UnityEngine;
using Zenject;

public class SlicerController : MonoBehaviour, IInitializable, ISlicerController
{
    [SerializeField] private MeshSlicerTest leftMeshSlicer;
    [SerializeField] private MeshSlicerTest rightMeshSlicer;


    public void Initialize()
    {

    }

    public void Slice()
    {
        leftMeshSlicer.Slice();
        rightMeshSlicer.Slice();
    }
    public void AssignCurrentSliceableTile(GroundTile currentTile)
    {
        leftMeshSlicer.sliceTarget = currentTile.gameObject;
        rightMeshSlicer.sliceTarget = currentTile.gameObject;
    }
}
