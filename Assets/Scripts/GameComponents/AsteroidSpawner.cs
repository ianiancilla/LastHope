using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] asteroidPrefabs;
    [SerializeField] private Transform[] spawnPoints;

    [SerializeField] private int maxAsteroidsOnScreen = 2;
    [SerializeField] private float spawnTimerMin;
    [SerializeField] private float spawnTimerMax;

    [Header("Cache")]
    [SerializeField] private Sector mySector;


    // members
    private float spawnTimer;
    private GameObject[] asteroidPool;
    private List<Transform> spawnPointsToCycle;
    private void Start()
    {
        InitialiseAsteroidPool();
        FillSpawnPointsToCycleList();

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
        if (spawnPointsToCycle.Count == 0)
        {
            FillSpawnPointsToCycleList();
        }
        var popped = spawnPointsToCycle[0];
        spawnPointsToCycle.RemoveAt(0);

        return popped.position;
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


            asteroidPool[i] = Instantiate(asteroidPrefab, 
                                            transform.position, 
                                            Quaternion.identity, 
                                            this.transform);

            asteroidPool[i].GetComponent<Asteroid>().SetSector(mySector);

            // asteroids need to be on the same sector layer,
            // for both collisions and camera culling
            Helpers.ChangeLayersRecursively(asteroidPool[i], gameObject.layer);

            asteroidPool[i].SetActive(false);
        }

    }

    private void FillSpawnPointsToCycleList()
    {
        spawnPointsToCycle = spawnPoints.ToList();
        spawnPointsToCycle = spawnPointsToCycle.OrderBy(x => Random.value).ToList();
    }
}
