using System;
using UnityEngine;
public interface IPlayerController
{
    Transform PlayerTransform { get; }
    void MovePlayerToPosition(Vector3 position, Action onCompleteCallBack = null);
    void PlayAnim(AnimationState animState);
    void RotateCamReferencePoint();
}
