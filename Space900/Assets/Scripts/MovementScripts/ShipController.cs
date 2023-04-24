/* SCRIPT ORIGINALE

using UnityEngine;
using UnityEngine.UI;

public class ShipController : MonoBehaviour
{
    public float maxSpeed = 15f;
    public float acceleration = 15f;
    public float deceleration = 15f;
    public Image joystickImage;
    public float joystickRadius = 50f;

    private Vector3 moveDirection;
    private Vector2 joystickStartPosition;
    private bool joystickActive;
    private int touchId;
    private float currentSpeed;

    private void Start()
    {
        joystickStartPosition = joystickImage.rectTransform.position;
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began && !joystickActive && RectTransformUtility.RectangleContainsScreenPoint(joystickImage.rectTransform, touch.position))
            {
                joystickActive = true;
                touchId = touch.fingerId;
            }

            if (touch.phase == TouchPhase.Moved && joystickActive && touch.fingerId == touchId)
            {
                Vector2 joystickPosition = touch.position - joystickStartPosition;
                float distanceFromCenter = joystickPosition.magnitude;

                if (distanceFromCenter > joystickRadius)
                {
                    joystickPosition = joystickPosition.normalized * joystickRadius;
                }

                float x = joystickPosition.x / joystickRadius;
                float z = joystickPosition.y / joystickRadius;

                moveDirection = new Vector3(x, 0f, z);
                moveDirection = Vector3.ClampMagnitude(moveDirection, 1f);
            }

            if (touch.phase == TouchPhase.Ended && joystickActive && touch.fingerId == touchId)
            {
                joystickActive = false;
                joystickImage.rectTransform.position = joystickStartPosition;
                moveDirection = Vector3.zero;
            }
        }

        currentSpeed = Mathf.MoveTowards(currentSpeed, joystickActive ? maxSpeed : 0f, acceleration * Time.deltaTime);
        transform.Translate(moveDirection * currentSpeed * Time.deltaTime, Space.World);
    }
}*/

/* SCRIPT MIGLIORATO CON ALCUNE MODIFICHE ALLE FUNZIONALITA`: RINUNCIA AD ALCUNE MECCANICHE DI GIOCO

using UnityEngine;
using UnityEngine.UI;

public class ShipController : MonoBehaviour
{
    public float maxSpeed = 50f;
    public float acceleration = 10f;
    public float deceleration = 8f;
    public Image joystickImage;
    public float joystickRadius = 50f;

    private Vector3 moveDirection;
    private Vector2 joystickStartPosition;
    private bool joystickActive;
    private int touchId;
    private float currentSpeed;

    private void Start()
    {
        joystickStartPosition = joystickImage.rectTransform.position;
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began && !joystickActive && RectTransformUtility.RectangleContainsScreenPoint(joystickImage.rectTransform, touch.position))
            {
                joystickActive = true;
                touchId = touch.fingerId;
            }

            if (touch.phase == TouchPhase.Moved && joystickActive && touch.fingerId == touchId)
            {
                Vector2 joystickPosition = touch.position - joystickStartPosition;
                float distanceFromCenter = joystickPosition.magnitude;

                if (distanceFromCenter > joystickRadius)
                {
                    joystickPosition = joystickPosition.normalized * joystickRadius;
                }

                float x = joystickPosition.x / joystickRadius;
                float z = joystickPosition.y / joystickRadius;

                moveDirection = new Vector3(x, 0f, z);
                moveDirection = Vector3.ClampMagnitude(moveDirection, 1f);
            }

            if (touch.phase == TouchPhase.Ended && joystickActive && touch.fingerId == touchId)
            {
                joystickActive = false;
                joystickImage.rectTransform.position = joystickStartPosition;
                moveDirection = Vector3.zero;
            }
        }

        if (joystickActive)
        {
            currentSpeed = Mathf.MoveTowards(currentSpeed, maxSpeed, acceleration * Time.deltaTime);
        }
        else
        {
            currentSpeed = Mathf.MoveTowards(currentSpeed, 0f, deceleration * Time.deltaTime);
        }

        transform.Translate(moveDirection * currentSpeed * Time.deltaTime, Space.World);
    }
} 
*/

/* SCRIPT AGGIORNATO CON AGGIUNTA DI RALLENTAMENTO DELLA NAVICELLA 

using UnityEngine;
using UnityEngine.UI;

public class ShipController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float maxSpeed = 15f;
    public float acceleration = 15f;
    public float deceleration = 15f;
    public float decelerationDelay = 0.5f;

    [Header("Joystick Settings")]
    public Image joystickImage;
    public float joystickRadius = 50f;

    private Vector3 moveDirection;
    private Vector2 joystickStartPosition;
    private bool joystickActive;
    private int touchId;
    private float currentSpeed;
    private float decelerationTimer;

    private void Start()
    {
        joystickStartPosition = joystickImage.rectTransform.position;
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began && !joystickActive && RectTransformUtility.RectangleContainsScreenPoint(joystickImage.rectTransform, touch.position))
            {
                joystickActive = true;
                touchId = touch.fingerId;
            }

            if (touch.phase == TouchPhase.Moved && joystickActive && touch.fingerId == touchId)
            {
                Vector2 joystickPosition = touch.position - joystickStartPosition;
                float distanceFromCenter = joystickPosition.magnitude;

                if (distanceFromCenter > joystickRadius)
                {
                    joystickPosition = joystickPosition.normalized * joystickRadius;
                }

                float x = joystickPosition.x / joystickRadius;
                float z = joystickPosition.y / joystickRadius;

                moveDirection = new Vector3(x, 0f, z);
                moveDirection = Vector3.ClampMagnitude(moveDirection, 1f);
            }

            if (touch.phase == TouchPhase.Ended && joystickActive && touch.fingerId == touchId)
            {
                joystickActive = false;
                joystickImage.rectTransform.position = joystickStartPosition;
                moveDirection = Vector3.zero;
                decelerationTimer = decelerationDelay;
            }
        }
        else
        {
            decelerationTimer -= Time.deltaTime;
            if (decelerationTimer <= 0f)
            {
                joystickActive = false;
                moveDirection = Vector3.zero;
                currentSpeed = Mathf.MoveTowards(currentSpeed, 0f, deceleration * Time.deltaTime);
            }
        }

        currentSpeed = Mathf.MoveTowards(currentSpeed, joystickActive ? maxSpeed : 0f, acceleration * Time.deltaTime);
        transform.Translate(moveDirection * currentSpeed * Time.deltaTime, Space.World);
    }
}
*/

/* SCRIPT CON MOVIMENTI PIU FLUIDI 

using UnityEngine;
using UnityEngine.UI;

public class ShipController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float maxSpeed = 20f;
    [SerializeField] private float acceleration = 5f;
    [SerializeField] private float deceleration = 1f;
    [SerializeField] private float decelerationDelay = 0.5f;

    [Header("Joystick Settings")]
    [SerializeField] private Image joystickImage;
    [SerializeField] private float joystickRadius = 50f;

    private Vector3 moveDirection;
    private Vector2 joystickStartPosition;
    private bool joystickActive;
    private int touchId;
    private float currentSpeed;
    private float decelerationTimer;

    private void Start()
    {
        joystickStartPosition = joystickImage.rectTransform.position;
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began && !joystickActive && RectTransformUtility.RectangleContainsScreenPoint(joystickImage.rectTransform, touch.position))
            {
                joystickActive = true;
                touchId = touch.fingerId;
            }

            if (touch.phase == TouchPhase.Moved && joystickActive && touch.fingerId == touchId)
            {
                Vector2 joystickPosition = touch.position - joystickStartPosition;
                float distanceFromCenter = joystickPosition.magnitude;

                if (distanceFromCenter > joystickRadius)
                {
                    joystickPosition = joystickPosition.normalized * joystickRadius;
                }

                float x = joystickPosition.x / joystickRadius;
                float z = joystickPosition.y / joystickRadius;

                moveDirection = new Vector3(x, 0f, z);
                moveDirection = Vector3.ClampMagnitude(moveDirection, 1f);
            }

            if (touch.phase == TouchPhase.Ended && joystickActive && touch.fingerId == touchId)
            {
                joystickActive = false;
                joystickImage.rectTransform.position = joystickStartPosition;
                moveDirection = Vector3.zero;
                decelerationTimer = decelerationDelay;
            }
        }
        else
        {
            decelerationTimer -= Time.deltaTime;
            if (decelerationTimer <= 0f)
            {
                joystickActive = false;
                moveDirection = Vector3.zero;
                currentSpeed = Mathf.MoveTowards(currentSpeed, 0f, deceleration * Time.deltaTime);
            }
        }

        if (joystickActive)
        {
            currentSpeed = Mathf.MoveTowards(currentSpeed, maxSpeed, acceleration * Time.deltaTime);
        }
        else
        {
            currentSpeed = Mathf.Lerp(currentSpeed, 0f, deceleration * Time.deltaTime);
        }

        transform.Translate(moveDirection * currentSpeed * Time.deltaTime, Space.World);
    }
}*/

/*

1 - VARIANTE

using UnityEngine;
using UnityEngine.UI;

public class ShipController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float maxSpeed = 20f;
    [SerializeField] private float acceleration = 5f;
    [SerializeField] private float deceleration = 5f;
    [SerializeField] private float decelerationDelay = 0.5f;

    [Header("Joystick Settings")]
    [SerializeField] private Image joystickImage;
    [SerializeField] private float joystickRadius = 50f;

    private Vector3 moveDirection;
    private Vector2 joystickStartPosition;
    private bool joystickActive;
    private int touchId;
    private float currentSpeed;
    private float decelerationTimer;

    private void Start()
    {
        joystickStartPosition = joystickImage.rectTransform.position;
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began && !joystickActive && RectTransformUtility.RectangleContainsScreenPoint(joystickImage.rectTransform, touch.position))
            {
                joystickActive = true;
                touchId = touch.fingerId;
            }

            if (touch.phase == TouchPhase.Moved && joystickActive && touch.fingerId == touchId)
            {
                Vector2 joystickPosition = touch.position - joystickStartPosition;
                float distanceFromCenter = joystickPosition.magnitude;

                if (distanceFromCenter > joystickRadius)
                {
                    joystickPosition = joystickPosition.normalized * joystickRadius;
                }

                float x = joystickPosition.x / joystickRadius;
                float z = joystickPosition.y / joystickRadius;

                moveDirection = new Vector3(x, 0f, z);
                moveDirection = Vector3.ClampMagnitude(moveDirection, 1f);
            }

            if (touch.phase == TouchPhase.Ended && joystickActive && touch.fingerId == touchId)
            {
                joystickActive = false;
                joystickImage.rectTransform.position = joystickStartPosition;
                moveDirection = Vector3.zero;
                decelerationTimer = decelerationDelay;
            }
        }
        else
        {
            joystickActive = false;
            moveDirection = Vector3.zero;
            currentSpeed = Mathf.MoveTowards(currentSpeed, 0f, deceleration * Time.deltaTime);
        }

        if (joystickActive)
        {
            currentSpeed = Mathf.MoveTowards(currentSpeed, maxSpeed, acceleration * Time.deltaTime);
        }
        else
        {
            if (decelerationTimer > 0f)
            {
                decelerationTimer -= Time.deltaTime;
            }
            else
            {
                currentSpeed = Mathf.MoveTowards(currentSpeed, 0f, deceleration * Time.deltaTime);
            }
        }

        transform.Translate(moveDirection * currentSpeed * Time.deltaTime, Space.World);
    }
}
*/

/*

2 - ALTRA VARIANTE 


using UnityEngine;
using UnityEngine.UI;

public class ShipController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float maxSpeed = 20f;
    [SerializeField] private float acceleration = 5f;
    [SerializeField] private float deceleration = 5f;
    [SerializeField] private float decelerationDelay = 0.5f;

    [Header("Joystick Settings")]
    [SerializeField] private Image joystickImage;
    [SerializeField] private float joystickRadius = 50f;

    private Vector3 moveDirection;
    private Vector2 joystickStartPosition;
    private bool joystickActive;
    private int touchId;
    private float currentSpeed;
    private float decelerationTimer;
    private bool touchEnded;

    private void Start()
    {
        joystickStartPosition = joystickImage.rectTransform.position;
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began && !joystickActive && RectTransformUtility.RectangleContainsScreenPoint(joystickImage.rectTransform, touch.position))
            {
                joystickActive = true;
                touchId = touch.fingerId;
                touchEnded = false;
            }

            if (touch.phase == TouchPhase.Moved && joystickActive && touch.fingerId == touchId)
            {
                Vector2 joystickPosition = touch.position - joystickStartPosition;
                float distanceFromCenter = joystickPosition.magnitude;

                if (distanceFromCenter > joystickRadius)
                {
                    joystickPosition = joystickPosition.normalized * joystickRadius;
                }

                float x = joystickPosition.x / joystickRadius;
                float z = joystickPosition.y / joystickRadius;

                moveDirection = new Vector3(x, 0f, z);
                moveDirection = Vector3.ClampMagnitude(moveDirection, 1f);
            }

            if (touch.phase == TouchPhase.Ended && joystickActive && touch.fingerId == touchId)
            {
                joystickActive = false;
                joystickImage.rectTransform.position = joystickStartPosition;
                moveDirection = Vector3.zero;
                decelerationTimer = decelerationDelay;
                touchEnded = true;
            }
        }
        else
        {
            if (joystickActive && !touchEnded)
            {
                decelerationTimer = decelerationDelay;
            }

            decelerationTimer -= Time.deltaTime;
            if (decelerationTimer <= 0f)
            {
                joystickActive = false;
                moveDirection = Vector3.zero;
                currentSpeed = Mathf.MoveTowards(currentSpeed, 0f, deceleration * Time.deltaTime);
            }
        }

        if (joystickActive || !touchEnded)
        {
            currentSpeed = Mathf.MoveTowards(currentSpeed, maxSpeed, acceleration * Time.deltaTime);
        }
        else
        {
            currentSpeed = Mathf.MoveTowards(currentSpeed, 0f, deceleration * Time.deltaTime);
        }

        transform.Translate(moveDirection * currentSpeed * Time.deltaTime, Space.World);
    }
}
*/

/* VARIANTE CON DUE JOYSTICK PER IL MOVIMENTO E LA VISUALE */
using UnityEngine;
using UnityEngine.UI;

public class ShipController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float maxSpeed = 20f;
    [SerializeField] private float acceleration = 5f;
    [SerializeField] private float deceleration = 5f;
    [SerializeField] private float decelerationDelay = 0.5f;

    [Header("Joystick Settings")]
    [SerializeField] private Image moveJoystickImage;
    [SerializeField] private float moveJoystickRadius = 50f;
    [SerializeField] private Image lookJoystickImage;
    [SerializeField] private float lookJoystickRadius = 50f;

    private Vector3 moveDirection;
    private Vector2 moveJoystickStartPosition;
    private bool moveJoystickActive;
    private int moveTouchId;
    private float currentSpeed;
    private float decelerationTimer;

    private Vector2 lookDirection;
    private Vector2 lookJoystickStartPosition;
    private bool lookJoystickActive;
    private int lookTouchId;

    private void Start()
    {
        moveJoystickStartPosition = moveJoystickImage.rectTransform.position;
        lookJoystickStartPosition = lookJoystickImage.rectTransform.position;
    }

    private void Update()
    {
        // Move joystick handling
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began && !moveJoystickActive && RectTransformUtility.RectangleContainsScreenPoint(moveJoystickImage.rectTransform, touch.position))
            {
                moveJoystickActive = true;
                moveTouchId = touch.fingerId;
            }

            if (touch.phase == TouchPhase.Moved && moveJoystickActive && touch.fingerId == moveTouchId)
            {
                Vector2 joystickPosition = touch.position - moveJoystickStartPosition;
                float distanceFromCenter = joystickPosition.magnitude;

                if (distanceFromCenter > moveJoystickRadius)
                {
                    joystickPosition = joystickPosition.normalized * moveJoystickRadius;
                }

                float x = joystickPosition.x / moveJoystickRadius;
                float z = joystickPosition.y / moveJoystickRadius;

                moveDirection = new Vector3(x, 0f, z);
                moveDirection = Vector3.ClampMagnitude(moveDirection, 1f);
            }

            if (touch.phase == TouchPhase.Ended && moveJoystickActive && touch.fingerId == moveTouchId)
            {
                moveJoystickActive = false;
                moveJoystickImage.rectTransform.position = moveJoystickStartPosition;
                moveDirection = Vector3.zero;
                decelerationTimer = decelerationDelay;
            }
        }
        else
        {
            decelerationTimer -= Time.deltaTime;
            if (decelerationTimer <= 0f)
            {
                moveJoystickActive = false;
                moveDirection = Vector3.zero;
                currentSpeed = Mathf.MoveTowards(currentSpeed, 0f, deceleration * Time.deltaTime);
            }
        }

        if (moveJoystickActive)
        {
            currentSpeed = Mathf.MoveTowards(currentSpeed, maxSpeed, acceleration * Time.deltaTime);
        }
        else
        {
            currentSpeed = Mathf.Lerp(currentSpeed, 0f, deceleration * Time.deltaTime);
        }

        // Look joystick handling
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began && !lookJoystickActive && RectTransformUtility.RectangleContainsScreenPoint(lookJoystickImage.rectTransform, touch.position))
            {
                lookJoystickActive = true;
                lookTouchId = touch.fingerId;
            }

            if (touch.phase == TouchPhase.Moved && lookJoystickActive && touch.fingerId == lookTouchId)
            {
                Vector2 joystickPosition = touch.position - lookJoystickStartPosition;
                float distanceFromCenter = joystickPosition.magnitude;

                if (distanceFromCenter > lookJoystickRadius)
                {
                    joystickPosition = joystickPosition.normalized * lookJoystickRadius;
                }

                float x = joystickPosition.x / lookJoystickRadius;
                float y = joystickPosition.y / lookJoystickRadius;

                lookDirection = new Vector2(x, y);
                transform.rotation = Quaternion.Euler(-y, x, 0f) * Quaternion.LookRotation(moveDirection);
            }

            if (touch.phase == TouchPhase.Ended && lookJoystickActive && touch.fingerId == lookTouchId)
            {
                lookJoystickActive = false;
                lookJoystickImage.rectTransform.position = lookJoystickStartPosition;
                lookDirection = Vector2.zero;
            }
        }
    }

    private void FixedUpdate()
    {
        transform.position += moveDirection * currentSpeed * Time.fixedDeltaTime;
    }
}