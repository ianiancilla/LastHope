using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] asteroidPrefabs;

    [SerializeField] private int maxAsteroidsOnScreen = 2;
    [SerializeField] private float spawnTimerMin;
    [SerializeField] private float spawnTimerMax;

    // cache
    BoxCollider2D boxCollider;

    // members
    private Vector2 boundMin;
    private Vector2 boundMax;
    private float spawnTimer;
    private GameObject[] asteroidPool;

    private void Start()
    {
        //cache
        boxCollider = GetComponent<BoxCollider2D>();

        // set spawn boundaries
        boundMin = boxCollider.bounds.min;
        boundMax = boxCollider.bounds.max;

        InitialiseAsteroidPool();

        // initialize timer
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
            ActivateAsteroid();
            SetSpawnTimer();
        }
    }

    private void ActivateAsteroid()
    {
        foreach (GameObject asteroid in asteroidPool)
        {
            // we need an asteroid that is currently in the pool, not in use
            if (asteroid.activeSelf) { continue; }

            Vector2 spawnPos = RandomizeAsteroidSpawnPoint();
            asteroid.transform.position = spawnPos;
            asteroid.SetActive(true);
            break;
        }
    }

    private Vector2 RandomizeAsteroidSpawnPoint()
    {
        // find random point in spawnbox collider
        return new Vector2(Random.Range(boundMin.x, boundMax.x),
                                        Random.Range(boundMin.y, boundMax.y));
    }

    private void SetSpawnTimer()
    {
        spawnTimer = Random.Range(spawnTimerMin, spawnTimerMax);
    }

    private void InitialiseAsteroidPool()
    {
        asteroidPool = new GameObject[maxAsteroidsOnScreen];

        for (int i = 0; i < maxAsteroidsOnScreen; i++)
        {
            GameObject asteroidPrefab = asteroidPrefabs[Random.Range(0, asteroidPrefabs.Length)];

            // find random point in spawnbox collider
            Vector2 spawnPos = new Vector2(Random.Range(boundMin.x, boundMax.x),
                                            Random.Range(boundMin.y, boundMax.y));

            asteroidPool[i] = Instantiate(asteroidPrefab, 
                                            spawnPos, 
                                            Quaternion.identity, 
                                            this.transform);

            // asteroids need to be on the same sector layer,
            // for both collisions and camera culling
            Helpers.ChangeLayersRecursively(asteroidPool[i], gameObject.layer);

            asteroidPool[i].SetActive(false);
        }

    }
}
