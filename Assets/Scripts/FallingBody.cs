using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class FallingBody : MonoBehaviour
{
    [SerializeField] private float fallingSpeedMax = 1.0f;
    [SerializeField] private float fallingSpeedMin = 1.0f;
    [SerializeField] private Vector2 movementNoiseSpeed;
    [SerializeField] private Vector2 movementNoiseStrength;


    private float fallingSpeed;
    private Vector2 currentXNoisePosition;
    private Vector2 currentYNoisePosition;


    private void Start()
    {
        fallingSpeed = UnityEngine.Random.Range(fallingSpeedMin, fallingSpeedMax);
        // assign random start to noise so even asteroids with the same noise level move differently
        currentXNoisePosition = new Vector2(Random.Range(0f, 1f), Random.Range(0f, 1f));
    }

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

    private Vector2 MovementNoise()
    {
        if (movementNoiseSpeed == Vector2.zero || movementNoiseStrength == Vector2.zero) { return Vector2.zero; }

        currentXNoisePosition += new Vector2(movementNoiseSpeed.x, movementNoiseSpeed.x) * Time.deltaTime;
        currentYNoisePosition += new Vector2(movementNoiseSpeed.y, movementNoiseSpeed.y) * Time.deltaTime;

        float noiseX = Mathf.PerlinNoise(currentXNoisePosition.x, currentXNoisePosition.y);
        float noiseY = Mathf.PerlinNoise(currentYNoisePosition.x, currentYNoisePosition.y);

        // PerlinNoise Return value might be slightly below 0.0 or beyond 1.0
        noiseX = Mathf.Clamp(noiseX, 0f, 1f);
        noiseY = Mathf.Clamp(noiseY, 0f, 1f);

        noiseX = Helpers.Remap(noiseX, 0f, 1f, -1f, 1f);
        noiseY = Helpers.Remap(noiseY, 0f, 1f, -1f, 1f);


        Vector2 noiseVector = new Vector2 (noiseX * movementNoiseStrength.x,
                                            noiseY * movementNoiseStrength.y);
        return noiseVector;
    }
}
