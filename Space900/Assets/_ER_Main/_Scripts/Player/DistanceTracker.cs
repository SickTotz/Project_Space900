using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DistanceTracker : MonoBehaviour
{
    public Transform spaceship;
    public TextMeshProUGUI distanceText;

    private int initialZPosition;
    private int distance;

    private void Start()
    {
        initialZPosition = Mathf.RoundToInt(spaceship.position.z);
    }

    private void Update()
    {
        distance = Mathf.RoundToInt(spaceship.position.z - initialZPosition);
        distanceText.text = distance.ToString() + " m";
    }
}