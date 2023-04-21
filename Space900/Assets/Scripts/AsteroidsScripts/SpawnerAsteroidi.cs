using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerAsteroidi : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Spawner Settings")]
    public GameObject cubePrefab;       
    public int cubeDensity;
    public int seed;
    public float ineerRadious;
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

}
