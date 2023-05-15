using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MOBILE_MovementMechanics : MonoBehaviour
{
    // Creazione di variabili per il movimento del player
    InputAction movement; //Movimento Navicella
    InputAction look;     //Movimento Visuale
    private float _xMovement, _yMovement;

    [Header("Joystick Settings")]
    public FixedJoystick Move_joystick;
    public FixedJoystick Look_joystick;

    [Header("Movement Settings")]
    public float forwardSpeed = 25f;
    public float strafeSpeed = 7.5f;

    // public float hoverSpeed = 5f;

    private float activeForwardSpeed;
    private float activeStrafeSpeed;
    private float activeHoverSpeed;
    public float lookRateSpeed = 90f;
    public float forwardAcceleration = 2.5f;
    public float strafeAcceleration = 2f;

    // public float hoverAcceleration = 2f;

    void Start()
    {
        // Inizializzazione della modalità Input Actions
        InitializeInputActions();

        // Inizializzazione dei joystick
        // Move_joystick = FindObjectOfType<FixedJoystick>();
        // Look_joystick = FindObjectOfType<FixedJoystick>();
    }

    void Update()
    {
        // Movimento della navicella
        MovePlayer();

        // Movimento della visuale
        MoveCamera();
    }

    // Inizializzazione delle Input Actions
    void InitializeInputActions()
    {
        movement = new InputAction("PlayerMovement", binding: "<Gamepad>/leftstick");
        movement.AddCompositeBinding("Dpad")
            .With("Up", "<keyboard>/w")
            .With("Down", "<keyboard>/s")
            .With("Left", "<keyboard>/a")
            .With("Right", "<keyboard>/d");

        movement.Enable();
        look = new InputAction("PlayerLook", binding: "<Gamepad>/rightstick");
        look.Enable();
    }

    // Movimento della navicella
    void MovePlayer()
    {
        // Input per il movimento della navicella
        float x = Move_joystick.Horizontal;
        float z = Move_joystick.Vertical;

        // Calcolo della velocità attiva
        activeForwardSpeed = Mathf.Lerp(activeForwardSpeed, z * forwardSpeed, forwardAcceleration * Time.deltaTime);
        activeStrafeSpeed = Mathf.Lerp(activeStrafeSpeed, x * strafeSpeed, strafeAcceleration * Time.deltaTime);

        // Movimento della navicella
        transform.position += transform.forward * activeForwardSpeed * Time.deltaTime;
        transform.position += transform.right * activeStrafeSpeed * Time.deltaTime;
    }

    // Movimento della visuale
    void MoveCamera()
    {
        // Movimento della visuale solo se ci sono touch in corso
        /*
        if (Touchscreen.current != null && Touchscreen.current.touches.Count > 0 && Touchscreen.current.touches[0].isInProgress)
        {
            // Movimento della visuale basato sul delta touch
            Vector2 touchDelta = Touchscreen.current.touches[0].delta.ReadValue();
            transform.Rotate(-touchDelta.y * Time.deltaTime * lookRateSpeed, touchDelta.x * Time.deltaTime * lookRateSpeed, 0, Space.Self);
        }
        */

        // Movimento della visuale
        float x = Look_joystick.Horizontal;
        float y = Look_joystick.Vertical;

        transform.Rotate(-y * lookRateSpeed * Time.deltaTime, x * lookRateSpeed * Time.deltaTime, 0, Space.Self);
    }
}