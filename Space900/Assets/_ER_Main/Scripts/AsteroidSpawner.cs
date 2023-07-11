// SPAWN AUMENTATO OGNI 1000 M PERCORSI DALLA NAVICELLA
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [Header("Get Player Position")]
    public Transform playerTransform;

    [Header("Asteroids List")]
    public List<GameObject> asteroidPrefabs;

    [Header("Asteroids Settings")]
    public float minSize = 0.5f;
    public float maxSize = 2f;
    public float minSpeed = 1f;
    public float maxSpeed = 5f;
    public float minRotationSpeed = 10f;
    public float maxRotationSpeed = 50f;
    public float initialSpawnInterval = 0.05f;
    public float yRange = 5f;
    public float zRange = 20f;

    private List<GameObject> activeAsteroids = new List<GameObject>();
    public float spawnInterval;
    public float distanceThreshold = 1000f;
    public float minSpawnInterval = 0.01f;

    private void Start()
    {
        playerTransform = transform;
        spawnInterval = initialSpawnInterval;
        StartCoroutine(SpawnAsteroids());
    }

    private IEnumerator SpawnAsteroids()
    {
        while (true)
        {
            GameObject asteroidPrefab = asteroidPrefabs[Random.Range(0, asteroidPrefabs.Count)];
            GameObject asteroid = Instantiate(asteroidPrefab);

            asteroid.transform.position = new Vector3(Random.Range(-yRange, yRange), Random.Range(-zRange, zRange), playerTransform.position.z);
            float size = Random.Range(minSize, maxSize);
            asteroid.transform.localScale = new Vector3(size, size, size);
            float speed = Random.Range(minSpeed, maxSpeed);
            asteroid.GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, -speed);
            float rotationSpeed = Random.Range(minRotationSpeed, maxRotationSpeed);
            asteroid.GetComponent<Rigidbody>().angularVelocity = Random.insideUnitSphere * rotationSpeed;

            activeAsteroids.Add(asteroid);

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void Update()
    {
        CheckAsteroidsOutOfBounds();
        UpdateSpawnInterval();
    }

    private void CheckAsteroidsOutOfBounds()
    {
        for (int i = 0; i < activeAsteroids.Count; i++)
        {
            GameObject asteroid = activeAsteroids[i];

            if (asteroid == null)
            {
                activeAsteroids.RemoveAt(i);
                i--; // Riduci l'indice per compensare la rimozione dell'elemento
                continue;
            }

            if (asteroid.transform.position.z < Camera.main.transform.position.z)
            {
                Destroy(asteroid);
                activeAsteroids.RemoveAt(i);
                i--; // Riduci l'indice per compensare la rimozione dell'elemento
            }
        }
    }

    private void UpdateSpawnInterval()
    {
        float distanceTraveled = Mathf.Abs(playerTransform.position.z);
        float intervalDecreaseRate = (initialSpawnInterval - minSpawnInterval) / distanceThreshold * 300;
        spawnInterval = Mathf.Max(initialSpawnInterval - intervalDecreaseRate * (distanceTraveled / 1000f), minSpawnInterval);
    }
}
