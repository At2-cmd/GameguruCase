using UnityEngine;

public class FallenCubeDeactivator : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out GroundTile groundTile))
        {
            groundTile.Despawn();
        }
    }
}
