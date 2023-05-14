/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MOBILE_MovementMechanics : MonoBehaviour
{   
    // Creazione di variabili per il movimento del player

    // Variabili Input Action
    InputAction movement;   //Movimento Navicella

    [Header("Movement Settings")]
    public float forwardSpeed = 25f, strafeSpeed = 7.5f, hoverSpeed = 5f;
    private float activeForwardSpeed, activeStrafeSpeed, activeHoverSpeed;
    private float forwardAcceleration = 2.5f, strafeAcceleration = 2f, hoverAcceleration = 2f;

    public float lookRateSpeed = 90f;
    private Vector2 lookInput, screenCenter, mouseDistance;

    private float rollInput;
    public float rollSpeed = 90f, rollAcceleration = 3.5f;

    float _xMovement, _yMovement;

    [Header("Joystick Settings")]
    public FixedJoystick joystick;

    // Start is called before the first frame update
    void Start()
    {   
        // Variabili per la definizione della visuale in gioco
        screenCenter.x = Screen.width * .5f;
        screenCenter.y = Screen.height * .5f;

        // Inizializzazione della modalita` Input Actions
        movement = new InputAction("PlayerMovement", binding: "<Gamepad>/leftstick");
        movement.AddCompositeBinding("Dpad")
                .With("Up", "<keyboard>/w")
                .With("Down", "<keyboard>/s")
                .With("Left", "<keyboard>/a")
                .With("Right", "<keyboard>/d");

        movement.Enable();
    }

    // Update is called once per frame
    void Update()
    {        
        // Impostazioni per la visualizzaione con il mouse
        lookInput.x = Input.mousePosition.x;
        lookInput.y = Input.mousePosition.y;

        float x = joystick.Horizontal;
        float z = joystick.Vertical;

        float mouseX = 0;
        float mouseY = 0;

        if(Touchscreen.current.touches.Count > 0 && Touchscreen.current.touches[0].isInProgress){
            mouseX = Touchscreen.current.touches[0].delta.ReadValue().x;
            mouseY = Touchscreen.current.touches[0].delta.ReadValue().y;
        }

        // Setting della velocita` massima del movimento da input col mouse
        mouseDistance.x = (lookInput.x - screenCenter.x) / screenCenter.y;
        mouseDistance.y = (lookInput.y - screenCenter.y) / screenCenter.y;

        mouseDistance = Vector2.ClampMagnitude(mouseDistance, 1f);

        rollInput = Mathf.Lerp(rollInput, Input.GetAxisRaw("Roll"), rollAcceleration * Time.deltaTime);

        transform.Rotate(-mouseDistance.y * lookRateSpeed * Time.deltaTime, mouseDistance.x * lookRateSpeed * Time.deltaTime, rollInput * rollSpeed * Time.deltaTime, Space.Self);

        // Utilizzo delle variabili per i movimenti del player
        activeForwardSpeed = Mathf.Lerp(activeForwardSpeed, z * forwardSpeed, forwardAcceleration * Time.deltaTime);
        activeStrafeSpeed = Mathf.Lerp(activeStrafeSpeed, x * strafeSpeed, strafeAcceleration * Time.deltaTime);
        activeHoverSpeed = Mathf.Lerp(activeHoverSpeed, Input.GetAxisRaw("Hover") * hoverSpeed, hoverAcceleration * Time.deltaTime);
        
        // Attribuizione del movimento al player
        transform.position += transform.forward * activeForwardSpeed * Time.deltaTime;
        transform.position += transform.right * activeStrafeSpeed * Time.deltaTime;
        transform.position += transform.up * activeHoverSpeed * Time.deltaTime; 
    }
}*/

//Variante per i controlli touchscreen
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MOBILE_MovementMechanics : MonoBehaviour
{   
    // Creazione di variabili per il movimento del player

    // Variabili Input Action
    InputAction movement;   //Movimento Navicella

    [Header("Movement Settings")]
    public float forwardSpeed = 25f, strafeSpeed = 7.5f, hoverSpeed = 5f;
    private float activeForwardSpeed, activeStrafeSpeed, activeHoverSpeed;
    private float forwardAcceleration = 2.5f, strafeAcceleration = 2f, hoverAcceleration = 2f;
    public float lookRateSpeed = 90f;
    float _xMovement, _yMovement;

    [Header("Joystick Settings")]
    public FixedJoystick joystick;

    // Start is called before the first frame update
    void Start()
    {   
        // Inizializzazione della modalita` Input Actions
        movement = new InputAction("PlayerMovement", binding: "<Gamepad>/leftstick");
        movement.AddCompositeBinding("Dpad")
                .With("Up", "<keyboard>/w")
                .With("Down", "<keyboard>/s")
                .With("Left", "<keyboard>/a")
                .With("Right", "<keyboard>/d");

        movement.Enable();
    }

    // Update is called once per frame
    void Update()
    {        
        // Impostazioni per la visualizzaione con il touchscreen
        float x = joystick.Horizontal;
        float z = joystick.Vertical;

        // Utilizzo delle variabili per i movimenti del player
        activeForwardSpeed = Mathf.Lerp(activeForwardSpeed, z * forwardSpeed, forwardAcceleration * Time.deltaTime);
        activeStrafeSpeed = Mathf.Lerp(activeStrafeSpeed, x * strafeSpeed, strafeAcceleration * Time.deltaTime);
        activeHoverSpeed = Mathf.Lerp(activeHoverSpeed, Input.GetAxisRaw("Hover") * hoverSpeed, hoverAcceleration * Time.deltaTime);
        
        // Attribuizione del movimento al player
        transform.position += transform.forward * activeForwardSpeed * Time.deltaTime;
        transform.position += transform.right * activeStrafeSpeed * Time.deltaTime;
        transform.position += transform.up * activeHoverSpeed * Time.deltaTime; 

        // Movimento della visuale
        if (Touchscreen.current != null && Touchscreen.current.touches.Count > 0 && Touchscreen.current.touches[0].isInProgress)
        {
            Vector2 touchDelta = Touchscreen.current.touches[0].delta.ReadValue();
            transform.Rotate(-touchDelta.y * Time.deltaTime * lookRateSpeed, touchDelta.x * Time.deltaTime * lookRateSpeed, 0, Space.Self);
        }
    }
}

// Altra variante
/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;

public class MOBILE_MovementMechanics : MonoBehaviour
{   
    // Creazione di variabili per il movimento del player

    // Variabili Input Action
    InputAction movement;   //Movimento Navicella

    [Header("Movement Settings")]
    public float forwardSpeed = 25f, strafeSpeed = 7.5f, hoverSpeed = 5f;
    private float activeForwardSpeed, activeStrafeSpeed, activeHoverSpeed;
    private float forwardAcceleration = 2.5f, strafeAcceleration = 2f, hoverAcceleration = 2f;
    public float lookRateSpeed = 90f;
    float _xMovement, _yMovement;

    [Header("Joystick Settings")]
    public FixedJoystick joystick;

    // Start is called before the first frame update
    void Start()
    {   
        // Inizializzazione della modalita` Input Actions
        movement = new InputAction("PlayerMovement", binding: "<Gamepad>/leftstick");
        movement.AddCompositeBinding("Dpad")
                .With("Up", "<keyboard>/w")
                .With("Down", "<keyboard>/s")
                .With("Left", "<keyboard>/a")
                .With("Right", "<keyboard>/d");

        movement.Enable();
    }

    // Update is called once per frame
    void Update()
    {        
        // Impostazioni per la visualizzaione con il touchscreen
        float x = joystick.Horizontal;
        float z = joystick.Vertical;

        // Utilizzo delle variabili per i movimenti del player
        activeForwardSpeed = Mathf.Lerp(activeForwardSpeed, z * forwardSpeed, forwardAcceleration * Time.deltaTime);
        activeStrafeSpeed = Mathf.Lerp(activeStrafeSpeed, x * strafeSpeed, strafeAcceleration * Time.deltaTime);
        activeHoverSpeed = Mathf.Lerp(activeHoverSpeed, Input.GetAxisRaw("Hover") * hoverSpeed, hoverAcceleration * Time.deltaTime);

        // Attribuizione del movimento al player
        transform.position += transform.forward * activeForwardSpeed * Time.deltaTime;
        transform.position += transform.right * activeStrafeSpeed * Time.deltaTime;
        transform.position += transform.up * activeHoverSpeed * Time.deltaTime;

        // Movimento della visuale
        if (Touchscreen.current != null && Touchscreen.current.touches.Count > 0)
        {
            // Se uno dei tocchi si sta muovendo, ruota la visuale
            foreach (UnityEngine.InputSystem.Controls.TouchControl touch in Touchscreen.current.touches)
            {
                if (touch.phase.ReadValue() == PointerPhase.Moved)
                {
                    // Calcola la delta del tocco rispetto alla posizione precedente
                    Vector2 touchDelta = touch.delta.ReadValue();

                    // Ruota la visuale
                    transform.parent.Rotate(-touchDelta.y * Time.deltaTime * lookRateSpeed, touchDelta.x * Time.deltaTime * lookRateSpeed, 0, Space.Self);
                }
            }
        }
    }
}
*/