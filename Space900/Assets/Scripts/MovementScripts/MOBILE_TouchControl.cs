using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mobile_TouchControls : MonoBehaviour
{
    // Creazione di variabili per il movimento del player
    public float forwardSpeed = 25f, strafeSpeed = 7.5f, hoverSpeed = 5f;
    private float activeForwardSpeed, activeStrafeSpeed, activeHoverSpeed;
    private float forwardAcceleration = 2.5f, strafeAcceleration = 2f, hoverAcceleration = 2f;

    public float lookRateSpeed = 90f;
    private Vector2 lookInput, screenCenter, mouseDistance;

    private float rollInput;
    public float rollSpeed = 90f, rollAcceleration = 3.5f;

    float _xMovement, _yMovement;

    // Start is called before the first frame update
    void Start()
    {
        // Variabili per la definizione della visuale di gioco
        screenCenter.x = Screen.width * .5f;
        screenCenter.y = Screen.height * .5f;
    }

    // Update is called once per frame
    void Update()
    {
        // Impostazioni per la visualizzaione con il touch
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.position.x < Screen.width / 2)
            {
                float direction = touch.position.y > screenCenter.y ? 1f : -1f;
                _yMovement = direction * hoverSpeed * Time.deltaTime;
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                _xMovement = touch.deltaPosition.x * strafeSpeed * Time.deltaTime;
                _yMovement = touch.deltaPosition.y * forwardSpeed * Time.deltaTime;
            }
        }
        else
        {
            _xMovement = 0f;
            _yMovement = 0f;
        }

        // Utilizzo delle variabili per i movimenti del player
        activeForwardSpeed = Mathf.Lerp(activeForwardSpeed, _yMovement, forwardAcceleration * Time.deltaTime);
        activeStrafeSpeed = Mathf.Lerp(activeStrafeSpeed, _xMovement, strafeAcceleration * Time.deltaTime);
        activeHoverSpeed = Mathf.Lerp(activeHoverSpeed, 0f, hoverAcceleration * Time.deltaTime);

        // Setting della velocita massima del movimento col touch
        mouseDistance = Vector2.ClampMagnitude(new Vector2(_xMovement, _yMovement), 1f);

        rollInput = Mathf.Lerp(rollInput, 0f, rollAcceleration * Time.deltaTime);
        transform.Rotate(-mouseDistance.y * lookRateSpeed * Time.deltaTime, mouseDistance.x * lookRateSpeed * Time.deltaTime, rollInput * rollSpeed * Time.deltaTime, Space.Self);

        // Attribuizione del movimento al player
        transform.position += transform.forward * activeForwardSpeed * Time.deltaTime;
        transform.position += transform.right * activeStrafeSpeed * Time.deltaTime;
        transform.position += transform.up * activeHoverSpeed * Time.deltaTime;
    }
}