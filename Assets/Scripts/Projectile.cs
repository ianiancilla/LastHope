using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    private Vector2 moveVector;

    void Update()
    {
        transform.position = new Vector2(transform.position.x + (moveVector.x * moveSpeed * Time.deltaTime),
                                        transform.position.y + (moveVector.y * moveSpeed * Time.deltaTime));
    }

    public void SetMoveVector(Vector2 direction)
    {
        moveVector = direction.normalized;
    }
}
