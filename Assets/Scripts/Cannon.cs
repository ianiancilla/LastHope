using UnityEngine;
using System.Collections.Generic;
using System;

public class Cannon : MonoBehaviour
{
    [SerializeField] Transform cannonSprite;
    [SerializeField] Transform projectileOrigin;
    [SerializeField] float aimingSpeed = 5f;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] float reloadTime;


    private Target currentTarget;
    private List<Target> targets = new List<Target>();
    private bool isLoaded;
    private float reloadTimer;

    private void Start()
    {
        FindFirstObjectByType<InputReader>().ShootInputSector1 += Shoot;

        isLoaded = true;
    }

    private void Update()
    {
        UpdateCurrentTarget();
        Aim();
        if (!isLoaded) { Reload(); }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.TryGetComponent<Target>(out Target target)) return;

        if (!targets.Contains(target))
        {
            targets.Add(target);
            target.OnTargetDestroyed += OnCurrentTargetDestroyed;
        }
    }

    private void OnCurrentTargetDestroyed(Target target)
    {
        targets.Remove(target);
        currentTarget = null;
    }
    private void UpdateCurrentTarget()
    {
        float targetDistanceFromGround = Mathf.Infinity;
        foreach (Target target in targets)
        {
            if (target.transform.position.y < targetDistanceFromGround)
            {
                currentTarget = target;
                targetDistanceFromGround = target.transform.position.y;
            }
        }
    }
    private void Aim()
    {
        if (currentTarget == null) { return; }
        cannonSprite.transform.up = Vector2.Lerp(cannonSprite.transform.up,
                            currentTarget.transform.position - transform.position,
                            Time.deltaTime * aimingSpeed);
            
    }

    private void Shoot()
    {
        if (!isLoaded) return;

        if (projectilePrefab == null) { return ; }
        
        GameObject projectileGO = Instantiate(projectilePrefab,
                                                projectileOrigin.position,
                                                Quaternion.identity,
                                                this.transform);
        projectileGO.GetComponent<Projectile>().SetMoveVector(cannonSprite.transform.up);
        isLoaded = false;
        reloadTimer = reloadTime;
    }

    private void Reload()
    {
        reloadTimer -= Time.deltaTime;
        if (reloadTimer < 0) { isLoaded = true; }
    }
}
