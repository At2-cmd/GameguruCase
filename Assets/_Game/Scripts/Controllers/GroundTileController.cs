using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GroundTileController : MonoBehaviour, IInitializable, IGroundTileController
{
    [Inject] GroundTile.Pool _groundTilesPool;
    [Inject] ISlicerController _sliceController;
    private Vector3 generationPosition;
    private GroundTile _currentGeneratedGroundTile;
    private float _generationXOffset = 2.5f;
    private int _generatedTileCount;
    public GroundTile CurrentGroundTile => _currentGeneratedGroundTile;

    public void Initialize()
    {
        GenerateGroundTile();
        _sliceController.AssignBackSideCube(_currentGeneratedGroundTile);
    }

    public GroundTile GenerateGroundTile()
    {
        _currentGeneratedGroundTile = _groundTilesPool.Spawn(transform.position + generationPosition);
        generationPosition.z += _currentGeneratedGroundTile.Length;
        generationPosition.x = _generationXOffset * (_generatedTileCount % 2 == 0 ? 1 : -1);
        _currentGeneratedGroundTile.transform.SetParent(transform);
        if (_sliceController.CurrentBackSideCube != null)
        {
            _currentGeneratedGroundTile.transform.localScale = 
                _sliceController.CurrentBackSideCube.transform.localScale;
            _currentGeneratedGroundTile.SetYoyoTarget(generationPosition.x);
            _currentGeneratedGroundTile.SetYoyoMovementStatus(true);
        }
        _generatedTileCount++;
        return _currentGeneratedGroundTile;
    }
}
