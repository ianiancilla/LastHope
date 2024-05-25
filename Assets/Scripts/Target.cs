using System;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] private Transform vfxParent;
    [SerializeField] private ParticleSystem explodeVFX;
    [SerializeField] private ParticleSystem groundHitVFX;

    public event Action<Target> OnTargetDestroyed;

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
        if (explodeVFX != null)
        {
            Instantiate(explodeVFX, transform.position, Quaternion.identity, vfxParent);
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
