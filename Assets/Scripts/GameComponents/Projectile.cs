using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float lifeSpan = 5f;

    private GameObject target;

    private Vector2 moveVector;

    void Update()
    {
        Move();
        Age();
    }

    private void Move()
    {
        //transform.position = new Vector2(transform.position.x + (moveVector.x * moveSpeed * Time.deltaTime),
        //                                transform.position.y + (moveVector.y * moveSpeed * Time.deltaTime));

        if (target == null)
        {
            Destroy(gameObject);
        }

        transform.position = Vector2.MoveTowards(transform.position,
                                                    target.transform.position,
                                                    moveSpeed * Time.deltaTime);
    }

    private void Age()
    {
        lifeSpan -= Time.deltaTime;
        if (lifeSpan < 0) { Destroy(gameObject); }
    }

    public void SetTarget(GameObject target)
    {
        this.target = target;
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
