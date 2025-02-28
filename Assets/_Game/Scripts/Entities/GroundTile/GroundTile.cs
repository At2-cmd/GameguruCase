using UnityEngine;

public class GroundTile : MonoBehaviour
{
    [SerializeField] private MeshRenderer meshRenderer;
    public float Length => meshRenderer.bounds.size.z;
}
