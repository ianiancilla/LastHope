using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float lifeSpan = 5f;

    private Vector2 moveVector;

    void Update()
    {
        Move();
        Age();
    }

    private void Move()
    {
        transform.position = new Vector2(transform.position.x + (moveVector.x * moveSpeed * Time.deltaTime),
                                        transform.position.y + (moveVector.y * moveSpeed * Time.deltaTime));
    }

    private void Age()
    {
        lifeSpan -= Time.deltaTime;
        if (lifeSpan < 0) { Destroy(gameObject); }
    }

    public void SetMoveVector(Vector2 direction)
    {
        moveVector = direction.normalized;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Asteroid>(out Asteroid target))
        {
            target.ShotDown();
            Destroy(gameObject);
        }
    }

}
