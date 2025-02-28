using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private const float _defaultTransitionDuration = 0.1f;

    private static readonly Dictionary<AnimationState, int> AnimationHashes = new()
    {
        { AnimationState.Idle, Animator.StringToHash("Idle") },
        { AnimationState.Run, Animator.StringToHash("Run") },
        { AnimationState.Dance, Animator.StringToHash("Dance") }
    };

    public void Initialize() { }

    public void PlayAnim(AnimationState state, float transitionTime = _defaultTransitionDuration)
    {
        if (AnimationHashes.TryGetValue(state, out int animHash))
        {
            animator.CrossFade(animHash, transitionTime);
        }
        else
        {
            Debug.LogWarning($"Animation hash for state {state} not found.");
        }
    }

    public void SetBlendValue(float value)
    {
        animator.SetFloat("BlendValue", value);
    }
}

public enum AnimationState
{
    Idle,
    Run,
    Dance
}
