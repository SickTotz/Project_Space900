/*

// SCRIPT ORIGINALE
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pianeta_SpawnerAsteroidi : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Spawner Settings")]
    public GameObject cubePrefab;       
    public int cubeDensity;
    public int seed;
    public float innerRadious;
    public float outerRadious;
    public float height;
    public bool rotatingClockwise;

    [Header("Asteroid Settings")]
    public float minOrbitSpeed;
    public float maxOrbitSpeed;
    public float minRotationSpeed;
    public float maxRotationSpeed;

    private Vector3 localPosition;
    private Vector3 worldOffet;
    private Vector3 worldPosition;

    private float randomRadius;
    private float randomRadian;
    private float x;
    private float y;
    private float z;

    private void Start(){

        Random.InitState(seed);

        for(int  i = 0; i<cubeDensity; i++){
            do{
                randomRadius = Random.Range(innerRadious, outerRadious);
                randomRadian = Random.Range(0, (2* Mathf.PI));

                x = randomRadius * Mathf.Cos(randomRadian);
                y = Random.Range(-(height/2), (height /2));
                z = randomRadius * Mathf.Sin(randomRadian);
            }while(float.IsNaN(z) && float.IsNaN(x));

            localPosition = new Vector3(x, y, z);
            worldOffet = transform.rotation * localPosition;
            worldPosition = transform.position + worldOffet;

            GameObject _asteroid = Instantiate(cubePrefab, worldPosition, Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360)));
            _asteroid.AddComponent<BeltObject>().SetupBeltObject(Random.Range(minOrbitSpeed, maxOrbitSpeed), Random.Range(minRotationSpeed, maxRotationSpeed), gameObject, rotatingClockwise);
            _asteroid.transform.SetParent(transform);
        }
    }
}
*/

// SCRIPT CON L'AGGIUNTA DI UNA DIM MINIMA E MASSIMA DEI SINGOLI ASTEROIDI
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pianeta_SpawnerAsteroidi : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Spawner Settings")]
    public GameObject cubePrefab;
    public int cubeDensity;
    public int seed;
    public float innerRadious;
    public float outerRadious;
    public float height;
    public bool rotatingClockwise;

    [Header("Asteroid Settings")]
    public float minOrbitSpeed;
    public float maxOrbitSpeed;
    public float minRotationSpeed;
    public float maxRotationSpeed;
    public float minSize;
    public float maxSize;

    private Vector3 localPosition;
    private Vector3 worldOffet;
    private Vector3 worldPosition;

    private float randomRadius;
    private float randomRadian;
    private float x;
    private float y;
    private float z;
    private float size;

    private void Start()
    {
        Random.InitState(seed);

        for (int i = 0; i < cubeDensity; i++)
        {
            do
            {
                randomRadius = Random.Range(innerRadious, outerRadious);
                randomRadian = Random.Range(0, (2 * Mathf.PI));

                x = randomRadius * Mathf.Cos(randomRadian);
                y = Random.Range(-(height / 2), (height / 2));
                z = randomRadius * Mathf.Sin(randomRadian);

                size = Random.Range(minSize, maxSize);
            } while (float.IsNaN(z) && float.IsNaN(x));

            localPosition = new Vector3(x, y, z);
            worldOffet = transform.rotation * localPosition;
            worldPosition = transform.position + worldOffet;

            GameObject _asteroid = Instantiate(cubePrefab, worldPosition, Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360)));
            _asteroid.transform.localScale = new Vector3(size, size, size);

            _asteroid.AddComponent<BeltObject>().SetupBeltObject(Random.Range(minOrbitSpeed, maxOrbitSpeed), Random.Range(minRotationSpeed, maxRotationSpeed), gameObject, rotatingClockwise);
            _asteroid.transform.SetParent(transform);
        }
    }
}