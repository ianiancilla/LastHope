using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] asteroidPrefabs;

    [SerializeField] private float spawnTimerMin;
    [SerializeField] private float spawnTimerMax;

    // cache
    BoxCollider2D boxCollider;

    // members
    private Vector2 boundMin;
    private Vector2 boundMax;
    private float spawnTimer;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        boundMin = boxCollider.bounds.min;
        boundMax = boxCollider.bounds.max;
        SetSpawnTimer();
    }

    private void Update()
    {
        SpawnTimerTick();
    }

    private void SpawnTimerTick()
    {
        spawnTimer -= Time.deltaTime;
        if (spawnTimer < 0)
        {
            SpawnAsteroid();
            SetSpawnTimer();
        }
    }

    private void SpawnAsteroid()
    {

        GameObject asteroidPrefab = asteroidPrefabs[Random.Range(0, asteroidPrefabs.Length)];

        // find random point in spawnbox collider
        Vector2 spawnPos = new Vector2(Random.Range(boundMin.x, boundMax.x),
                                        Random.Range(boundMin.y, boundMax.y));
        
        GameObject asteroidGO = Instantiate(asteroidPrefab, spawnPos, Quaternion.identity, this.transform);

        // asteroids need to be on the same sector layer, for both collisions and camera culling
        Helpers.ChangeLayersRecursively(asteroidGO, gameObject.layer);
    }

    private void SetSpawnTimer()
    {
        spawnTimer = Random.Range(spawnTimerMin, spawnTimerMax);
    }
}
