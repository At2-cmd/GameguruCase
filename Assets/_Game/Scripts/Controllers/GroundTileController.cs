using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GroundTileController : MonoBehaviour, IInitializable, IGroundTileController
{
    [Inject] ISlicerController _slicerController;
    [SerializeField] private GroundTile groundTilePrefab;
    private float groundTileLength;
    private Vector3 generationPosition;

    public void Initialize()
    {
        groundTileLength = groundTilePrefab.Length;
        GenerateGroundTile();
    }

    public void GenerateGroundTile()
    {
        generationPosition.z += groundTileLength;
        GameObject groundTile = Instantiate(groundTilePrefab.gameObject, transform.position + generationPosition, Quaternion.identity);
        groundTile.transform.SetParent(transform);
        _slicerController.AssignCurrentSliceableTile(groundTile.GetComponent<GroundTile>());
    }
}
