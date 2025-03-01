using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GroundTileController : MonoBehaviour, IInitializable, IGroundTileController
{
    [Inject] GroundTile.Pool _groundTilesPool;
    private Vector3 generationPosition;

    public void Initialize()
    {
        GenerateGroundTile();
    }

    public void GenerateGroundTile()
    {
        GroundTile groundTile = _groundTilesPool.Spawn(transform.position + generationPosition);
        generationPosition.z += groundTile.Length;
        groundTile.transform.SetParent(transform);
    }
}
