// ORIGINALE
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{

    [Header("Asteroids List")]
    public List<GameObject> asteroidPrefabs;

    [Header("Asteroids Settings")]
    public float minSize = 0.5f;
    public float maxSize = 2f;
    public float minSpeed = 1f;
    public float maxSpeed = 5f;
    public float minRotationSpeed = 10f;
    public float maxRotationSpeed = 50f;
    public float spawnInterval = 2f;
    public float yRange = 5f;
    public float zRange = 20f;

    private List<GameObject> activeAsteroids = new List<GameObject>();
    private List<GameObject> asteroidPool = new List<GameObject>();

    private void Start()
    {
        InitializeAsteroidPool();
        StartCoroutine(SpawnAsteroids());
    }

    private void InitializeAsteroidPool()
    {
        for (int i = 0; i < asteroidPrefabs.Count; i++)
        {
            GameObject asteroidPrefab = asteroidPrefabs[i];

            for (int j = 0; j < 5; j++) // Creazione di 5 asteroidi per ogni prefab nel pool
            {
                GameObject asteroid = Instantiate(asteroidPrefab);
                asteroid.SetActive(false); // Disattiva gli asteroidi all'inizio
                asteroidPool.Add(asteroid);
            }
        }
    }

    private IEnumerator SpawnAsteroids()
    {
        while (true)
        {
            GameObject asteroidPrefab = asteroidPrefabs[Random.Range(0, asteroidPrefabs.Count)];
            GameObject asteroid = GetPooledAsteroid(asteroidPrefab); // Ottieni un asteroide dal pool

            asteroid.transform.position = new Vector3(Random.Range(-yRange, yRange), Random.Range(-zRange, zRange), transform.position.z);
            float size = Random.Range(minSize, maxSize);
            asteroid.transform.localScale = new Vector3(size, size, size);
            float speed = Random.Range(minSpeed, maxSpeed);
            asteroid.GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, -speed);
            float rotationSpeed = Random.Range(minRotationSpeed, maxRotationSpeed);
            asteroid.GetComponent<Rigidbody>().angularVelocity = Random.insideUnitSphere * rotationSpeed;

            asteroid.SetActive(true); // Attiva l'asteroide dal pool

            activeAsteroids.Add(asteroid);

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private GameObject GetPooledAsteroid(GameObject prefab)
    {
        for (int i = 0; i < asteroidPool.Count; i++)
        {
            if (!asteroidPool[i].activeInHierarchy && asteroidPool[i].CompareTag(prefab.tag))
            {
                return asteroidPool[i];
            }
        }

        GameObject asteroid = Instantiate(prefab);
        asteroid.SetActive(false);
        asteroidPool.Add(asteroid);
        return asteroid;
    }

    private void Update()
    {
        CheckAsteroidsOutOfBounds();
    }

    private void CheckAsteroidsOutOfBounds()
    {
        for (int i = activeAsteroids.Count - 1; i >= 0; i--)
        {
            GameObject asteroid = activeAsteroids[i];

            if (asteroid.transform.position.z < Camera.main.transform.position.z)
            {
                asteroid.SetActive(false); // Disattiva l'asteroide invece di distruggerlo
                activeAsteroids.RemoveAt(i);
            }
        }
    }
}