using System;
using UnityEngine;

public class FallingBody : MonoBehaviour
{
    [SerializeField] private float fallingSpeedMax = 1.0f;
    [SerializeField] private float fallingSpeedMin = 1.0f;
    //[SerializeField] private float movementNoise;

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
}
