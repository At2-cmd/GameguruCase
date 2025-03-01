using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    private bool _canMove;
    public void Initialize(){}
    public void SetMovementStatus(bool canMove)
    {
        _canMove = canMove;
    }
}
