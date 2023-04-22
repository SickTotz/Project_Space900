using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchMovementMechanics : MonoBehaviour
{
    public float forwardSpeed = 25f, strafeSpeed = 7.5f, hoverSpeed = 5f;
    private float activeForwardSpeed, activeStrafeSpeed, activeHoverSpeed;
    private float forwardAcceleration = 2.5f, strafeAcceleration = 2f, hoverAcceleration = 2f;

    public float lookRateSpeed = 90f;
    private Vector2 lookInput, screenCenter, mouseDistance;

    private float rollInput;
    public float rollSpeed = 90f, rollAcceleration = 3.5f;

    public float touchDeadZone = 30f;
    private Vector2 touchOrigin;
    private bool touchToMove = false;

    float _xMovement, _yMovement;

    void Start()
    {
        screenCenter.x = Screen.width * .5f;
        screenCenter.y = Screen.height * .5f;
    }

    void Update()
    {
        if (Input.touches.Length > 0)
        {
            foreach (Touch touch in Input.touches)
            {
                if (touch.phase == TouchPhase.Began)
                {
                    if (touch.position.x < Screen.width / 2)
                    {
                        touchOrigin = touch.position;
                        touchToMove = true;
                    }
                    else
                    {
                        lookInput.x = touch.position.x;
                        lookInput.y = touch.position.y;
                    }
                }
                else if (touch.phase == TouchPhase.Moved && touchToMove)
                {
                    Vector2 touchEnd = touch.position;

                    float x = touchEnd.x - touchOrigin.x;
                    float y = touchEnd.y - touchOrigin.y;

                    touchOrigin = touchEnd;

                    if (Mathf.Abs(x) > Mathf.Abs(y))
                    {
                        activeStrafeSpeed = x > 0 ? strafeSpeed : -strafeSpeed;
                        activeForwardSpeed = 0f;
                    }
                    else
                    {
                        activeForwardSpeed = y > 0 ? forwardSpeed : -forwardSpeed;
                        activeStrafeSpeed = 0f;
                    }
                }
                else if (touch.phase == TouchPhase.Ended)
                {
                    activeForwardSpeed = 0f;
                    activeStrafeSpeed = 0f;

                    touchToMove = false;
                }
                else if (touch.phase == TouchPhase.Stationary && touchToMove)
                {
                    activeForwardSpeed = 0f;
                    activeStrafeSpeed = 0f;
                }

                if (touch.phase == TouchPhase.Moved && !touchToMove)
                {
                    lookInput.x = touch.position.x;
                    lookInput.y = touch.position.y;
                }
            }
        }

        mouseDistance.x = (lookInput.x - screenCenter.x) / screenCenter.y;
        mouseDistance.y = (lookInput.y - screenCenter.y) / screenCenter.y;
        mouseDistance = Vector2.ClampMagnitude(mouseDistance, 1f);

        rollInput = Mathf.Lerp(rollInput, Input.GetAxisRaw("Roll"), rollAcceleration * Time.deltaTime);

        transform.Rotate(-mouseDistance.y * lookRateSpeed * Time.deltaTime, mouseDistance.x * lookRateSpeed * Time.deltaTime, rollInput * rollSpeed * Time.deltaTime, Space.Self);

        activeHoverSpeed = Mathf.Lerp(activeHoverSpeed, Input.GetAxisRaw("Hover") * hoverSpeed, hoverAcceleration * Time.deltaTime);

        transform.position += transform.forward * activeForwardSpeed * Time.deltaTime;
        transform.position += transform.right * activeStrafeSpeed * Time.deltaTime;
        transform.position += transform.up * activeHoverSpeed * Time.deltaTime;
    }
}
