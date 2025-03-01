using UnityEngine;
public interface IPlayerController
{
    void MovePlayerToLastTile(Vector3 lastTilePosition);
    void PlayAnim(AnimationState animState);
}
