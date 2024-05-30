using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class RotatingBody : MonoBehaviour
{
    [SerializeField] [Range(0, 30)] private float maxRotationSpeed;

    private float rotationSpeed;

    void Start()
    {
        rotationSpeed = UnityEngine.Random.Range(-maxRotationSpeed, maxRotationSpeed);
    }

    void Update()
    {
        Rotate();
    }

    private void Rotate()
    {
        transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
    }

}
