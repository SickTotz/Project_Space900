using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotalSpace_SpawnerAsteroidi : MonoBehaviour {

    [SerializeField] private List<GameObject> asteroidPrefabs;
    public float density = 1f;
    public float minSize = 0.5f;
    public float maxSize = 2f;
    public float spawnDistanceMin = 40f;
    public float spawnDistanceMax = 60f;
    public float asteroidSpeed = 10f;
    public float rotationSpeed = 10f;
    public float proximityDistance = 20f;
    private List<GameObject> asteroids = new List<GameObject>();

    // Start is called before the first frame update
    void Start() {
        SpawnAsteroids();
    }

    // Update is called once per frame
    void Update() {
        MoveAsteroids();
    }

    private void SpawnAsteroids() {
        int asteroidsCount = (int)(density * (spawnDistanceMax - spawnDistanceMin));
        for (int i = 0; i < asteroidsCount; i++) {
            Vector3 asteroidPos = Random.onUnitSphere * Random.Range(spawnDistanceMin, spawnDistanceMax);
            Quaternion asteroidRot = Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
            GameObject asteroidPrefab = asteroidPrefabs[Random.Range(0, asteroidPrefabs.Count)];
            GameObject asteroid = Instantiate(asteroidPrefab, asteroidPos, asteroidRot);
            float asteroidSize = Random.Range(minSize, maxSize);
            asteroid.transform.localScale = Vector3.one * asteroidSize;
            asteroids.Add(asteroid);

            if (Vector3.Distance(asteroid.transform.position, transform.position) < proximityDistance) {
                asteroid.transform.position = ((asteroid.transform.position - transform.position).normalized * proximityDistance) + transform.position;
            }

            Rigidbody asteroidBody = asteroid.GetComponent<Rigidbody>();
            asteroidBody.angularVelocity = Random.onUnitSphere * rotationSpeed;
            asteroidBody.useGravity = false;
        }
    }

    private void MoveAsteroids() {
        foreach (GameObject asteroid in asteroids) {
            asteroid.transform.position += asteroid.transform.forward * asteroidSpeed * Time.deltaTime;
            asteroid.transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        }
    }
}
