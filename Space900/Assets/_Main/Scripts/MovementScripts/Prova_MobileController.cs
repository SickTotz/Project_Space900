using UnityEngine.UI;
using UnityEngine;

public class Prova_MobileController : MonoBehaviour
{

    [Header("Movement Settings")]

    public float forwardSpeed = 25f, strafeSpeed = 7.5f, hoverSpeed = 5f;
    private float activeForwardSpeed, activeStrafeSpeed, activeHoverSpeed;
    private float forwardAcceleration = 2.5f, strafeAcceleration = 2f, hoverAcceleration = 2f;

    public float lookRateSpeed = 90f;
    private Vector2 lookInput, screenCenter;

    private float rollInput;
    public float rollSpeed = 90f, rollAcceleration = 3.5f;

    float _xMovement, _yMovement;

    [Header("Joystick Settings")]
    [SerializeField] private Image joystickImage;

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
