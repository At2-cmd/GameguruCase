using UnityEngine;

public class CubeSlicer : MonoBehaviour
{
    public GameObject cubePrefab;
    public Transform frontCube;
    public Transform backCube;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SliceCube();
        }
    }

    void SliceCube()
    {
        float frontXMin = frontCube.position.x - frontCube.localScale.x / 2;
        float frontXMax = frontCube.position.x + frontCube.localScale.x / 2;
        float backXMin = backCube.position.x - backCube.localScale.x / 2;
        float backXMax = backCube.position.x + backCube.localScale.x / 2;

        if (frontXMin < backXMin || frontXMax > backXMax)
        {
            float overlapStart = Mathf.Max(frontXMin, backXMin);
            float overlapEnd = Mathf.Min(frontXMax, backXMax);
            float overlapSize = overlapEnd - overlapStart;

            frontCube.position = new Vector3(overlapStart + overlapSize / 2, frontCube.position.y, frontCube.position.z);
            frontCube.localScale = new Vector3(overlapSize, frontCube.localScale.y, frontCube.localScale.z);

            // Create excess part with Rigidbody to drop
            float excessSize = Mathf.Abs(frontXMax - backXMax) > 0.01f ? Mathf.Abs(frontXMax - backXMax) : Mathf.Abs(frontXMin - backXMin);
            if (excessSize > 0.01f)
            {
                float excessX = (frontXMax > backXMax) ? frontXMax - excessSize / 2 : frontXMin + excessSize / 2;
                GameObject excessCube = Instantiate(cubePrefab, new Vector3(excessX, frontCube.position.y, frontCube.position.z), Quaternion.identity);
                excessCube.transform.localScale = new Vector3(excessSize, frontCube.localScale.y, frontCube.localScale.z);

                Rigidbody rb = excessCube.AddComponent<Rigidbody>();
                rb.mass = 1f;
            }
        }
    }
}