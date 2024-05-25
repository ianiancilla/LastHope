using System;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] private ParticleSystem explodeVFX;
    [SerializeField] private ParticleSystem groundHitVFX;

    private AsteroidSpawner mySpawner;

    public event Action<Target> OnTargetDestroyed;

    // cache
    Transform vfxParent;

    private void Start()
    {
        vfxParent = GameObject.Find("VFXParent").transform;
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
        if (groundHitVFX != null) 
        {
            GameObject groundHitGO = Instantiate(groundHitVFX, transform.position, Quaternion.identity, vfxParent).gameObject;

            // spawns need to be on the same sector layer, for both collisions and camera culling
            Helpers.ChangeLayersRecursively(groundHitGO, gameObject.layer);
        }

        Explode();
    }

    private void Explode()
    {
        if (explodeVFX != null)
        {
            GameObject explodeGO = Instantiate(explodeVFX, transform.position, Quaternion.identity, vfxParent).gameObject;
            // spawns need to be on the same sector layer, for both collisions and camera culling
            Helpers.ChangeLayersRecursively(explodeGO, gameObject.layer);

        }
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        OnTargetDestroyed?.Invoke(this);
    }

    public void ShotDown()
    {
        Explode();
    }
}