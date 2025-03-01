using System.Collections;
public interface IGroundTileController
{
    GroundTile GenerateGroundTile();
    GroundTile CurrentGroundTile { get; }
}
