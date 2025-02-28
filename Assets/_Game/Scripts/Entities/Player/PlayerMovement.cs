using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    private bool _canMove;
    public void Initialize()
    {

    }
    public void Update()
    {
        if (!_canMove) return;
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
    public void SetMovementStatus(bool canMove)
    {
        _canMove = canMove;
    }
}
