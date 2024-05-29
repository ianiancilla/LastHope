using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] AnimationCurve smallShakeCurve;
    [SerializeField] float smallShakeDuration = 0.5f;
    [SerializeField] AnimationCurve largeShakeCurve;
    [SerializeField] float largeShakeDuration = 1f;

    [SerializeField] Sector mySector;

    public void OnEnable()
    {
        mySector.OnCannonShoot += SmallShake;
        mySector.OnSectorHit += LargeShake;
        mySector.OnSectorKilled += LargeShake;
    }

    public void OnDisable()
    {
        mySector.OnCannonShoot -= SmallShake;
        mySector.OnSectorKilled -= LargeShake;
    }

    IEnumerator Shake (AnimationCurve curve, float duration)
    {
        Vector3 camStartPos = transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float shakeStrength = curve.Evaluate(elapsedTime/duration);
            transform.position = camStartPos + Random.insideUnitSphere * shakeStrength;
            yield return null;
        }
        transform.position = camStartPos;
    }

    private void SmallShake()
    {
        StartCoroutine(Shake(smallShakeCurve, smallShakeDuration));
    }

    private void LargeShake()
    {
        StartCoroutine(Shake(largeShakeCurve, largeShakeDuration));
    }

}
