using UnityEngine;
using UnityEngine.UI;

public class ShipController : MonoBehaviour
{
    public float maxSpeed = 10f;
    public float acceleration = 5f;
    public float deceleration = 2f;

    private Rigidbody shipRigidbody;
    private Vector3 velocity;
    private float speed;

    public RectTransform joystickBackground;
    public RectTransform joystickHandle;

    private bool isMoving;
    private Vector2 joystickStartPosition;

    private void Start()
    {
        shipRigidbody = GetComponent<Rigidbody>();
        joystickStartPosition = joystickHandle.position;
    }

    private void FixedUpdate()
    {
        if (isMoving)
        {
            Vector2 joystickOffset = (Vector2)joystickHandle.position - joystickStartPosition;
            float xMovement = joystickOffset.x / joystickBackground.sizeDelta.x;
            float yMovement = joystickOffset.y / joystickBackground.sizeDelta.y;
            Vector3 movement = new Vector3(xMovement, 0f, yMovement);

            speed = Mathf.Clamp(movement.magnitude, 0f, 1f) * maxSpeed;
            movement = transform.TransformDirection(movement);

            if (speed > 0f)
            {
                shipRigidbody.AddForce(movement * acceleration * speed, ForceMode.Acceleration);
            }
        }
        else
        {
            speed = Mathf.Lerp(speed, 0f, deceleration * Time.fixedDeltaTime);
        }

        shipRigidbody.velocity = transform.forward * speed;
    }

    public void StartMoving()
    {
        isMoving = true;
    }

    public void StopMoving()
    {
        isMoving = false;
    }

    public void ResetJoystick()
    {
        joystickHandle.position = joystickStartPosition;
    }
}
