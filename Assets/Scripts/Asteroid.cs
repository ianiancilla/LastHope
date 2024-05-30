using System;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField] private ParticleSystem explodeVFX;
    [SerializeField] private GameObject groundHitVFX;

    [Header("Cache")]
    [SerializeField] private Sector mySector;
    public void SetSector(Sector sector) { mySector = sector; }

    //events
    public event Action<Asteroid> OnAsteroidExplode;

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
            Vector2 hitGroundPos = transform.position;
            hitGroundPos.y = -4.5f;
            GameObject groundHitGO = Instantiate(groundHitVFX, hitGroundPos, Quaternion.identity, mySector.VFXParent);

            // spawns need to be on the same sector layer, for both collisions and camera culling
            Helpers.ChangeLayersRecursively(groundHitGO, gameObject.layer);
        }

        Explode();
    }

    private void Explode()
    {
        if (explodeVFX != null)
        {
            GameObject explodeGO = Instantiate(explodeVFX, transform.position, Quaternion.identity, mySector.VFXParent).gameObject;
            // spawns need to be on the same sector layer, for both collisions and camera culling
            Helpers.ChangeLayersRecursively(explodeGO, gameObject.layer);

        }
        OnAsteroidExplode?.Invoke(this);
        gameObject.SetActive(false);
    }

    public void ShotDown()
    {
        Explode();
    }
}
