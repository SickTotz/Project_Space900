using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public List<GameObject> asteroidPrefabs;
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

    private void Start()
    {
        StartCoroutine(SpawnAsteroids());
    }

    private IEnumerator SpawnAsteroids()
    {
        while (true)
        {
            GameObject asteroidPrefab = asteroidPrefabs[Random.Range(0, asteroidPrefabs.Count)];
            GameObject asteroid = Instantiate(asteroidPrefab);
            asteroid.transform.position = new Vector3(Random.Range(-yRange, yRange), Random.Range(-zRange, zRange), transform.position.z);
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
    }

    private void CheckAsteroidsOutOfBounds()
    {
        for (int i = activeAsteroids.Count - 1; i >= 0; i--)
        {
            GameObject asteroid = activeAsteroids[i];

            if (asteroid.transform.position.z < Camera.main.transform.position.z)
            {
                Destroy(asteroid);
                activeAsteroids.RemoveAt(i);
            }
        }
    }
}
