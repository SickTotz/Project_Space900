using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement_Navicella : MonoBehaviour
{   
    // Creazione di variabili per il movimento del player
    public float forwardSpeed = 25f, strafeSpeed = 7.5f, hoverSpeed = 5f;
    private float activeForwardSpeed, activeStrafeSpeed, activeHoverSpeed;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Utilizzo delle variabili per i movimenti del player
        activeForwardSpeed = Input.GetAxisRaw("Vertical") * forwardSpeed;
        activeStrafeSpeed = Input.GetAxisRaw("Horizontal") * strafeSpeed;
        activeHoverSpeed = Input.GetAxisRaw("Hover") * hoverSpeed;

        // Attribuizione del movimento al player
        transform.position += transform.forward * activeForwardSpeed * Time.deltaTime;
    }
}
