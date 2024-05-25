using UnityEngine;
using System.Collections.Generic;
using System;

public class Cannon : MonoBehaviour
{
    [SerializeField] Transform cannonSprite;

    private Target currentTarget;
    private List<Target> targets = new List<Target>();

    private void Update()
    {
        UpdateCurrentTarget();
        Aim();
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
        float targetDistance = Mathf.Infinity;
        foreach (Target target in targets)
        {
            if (Vector2.Distance(transform.position, target.transform.position) < targetDistance)
            {
                currentTarget = target;
            }
        }
    }
    private void Aim()
    {
        if (currentTarget == null) { return; }
        cannonSprite.transform.up = currentTarget.transform.position - transform.position;
    }
}
