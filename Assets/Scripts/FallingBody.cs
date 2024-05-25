using System;
using UnityEngine;

public class FallingBody : MonoBehaviour
{
    [SerializeField] private float fallingSpeedMax = 1.0f;
    [SerializeField] private float fallingSpeedMin = 1.0f;
    //[SerializeField] private float movementNoise;

    [SerializeField] private Transform vfxParent;
    [SerializeField] private ParticleSystem explodeVFX;
    [SerializeField] private ParticleSystem groundHitVFX;

    private float fallingSpeed;

    private void Start()
    {
        fallingSpeed = UnityEngine.Random.Range(fallingSpeedMax, fallingSpeedMin);
    }
    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector2 newPos = transform.position;
        newPos.y -= fallingSpeed * Time.deltaTime;

        newPos += MovementNoise();

        transform.position = newPos;
    }

    // TODO add noise function
    private Vector2 MovementNoise()
    {
        //if (movementNoise == 0) { return Vector2.zero; }

        return Vector2.zero;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Ground>(out Ground ground))
        {
            HittingGround(ground);
        }
    }

    private void HittingGround(Ground ground)
    {
        ground.HitByAsteroid();
        if (groundHitVFX != null) { Instantiate(groundHitVFX, transform.position, Quaternion.identity, vfxParent); }
        Explode();
    }

    private void Explode()
    {
        if (explodeVFX != null) {
            Debug.Log("BOOM");
            Instantiate(explodeVFX, transform.position, Quaternion.identity, vfxParent); }
        Destroy(gameObject);
    }
}
