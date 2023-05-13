using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    [SerializeField] private float speed = 5.0f;
    [SerializeField] private float rotationSpeed = 30.0f;

    private RectTransform joystickBackground;
    private RectTransform joystickHandle;
    private Vector2 joystickPosition;

    private bool isJoystickPressed = false;
    private bool isRotating = false;
    private float initialRotationAngle;
    private Vector2 initialTouchPosition;

    void Start()
    {
        joystickBackground = GameObject.Find("JoystickBackground").GetComponent<RectTransform>();
        joystickHandle = GameObject.Find("JoystickHandle").GetComponent<RectTransform>();
    }

    void Update()
    {
        if (isJoystickPressed)
        {
            float horizontalInput = (joystickPosition.x - joystickBackground.position.x) / (joystickBackground.rect.width / 2);
            float verticalInput = (joystickPosition.y - joystickBackground.position.y) / (joystickBackground.rect.height / 2);

            Vector2 movement = new Vector2(horizontalInput, verticalInput);
            transform.Translate(movement * speed * Time.deltaTime, Space.World);
        }

        if (isRotating)
        {
            float distanceFromCenter = (initialTouchPosition.y - Input.mousePosition.y) / (Screen.height / 2);
            float rotationAngle = initialRotationAngle + distanceFromCenter * rotationSpeed;

            transform.rotation = Quaternion.Euler(rotationAngle, rotationAngle, 0);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (RectTransformUtility.RectangleContainsScreenPoint(joystickBackground, eventData.position))
        {
            isJoystickPressed = true;
            joystickPosition = eventData.position;
        }
        else
        {
            isRotating = true;
            initialRotationAngle = transform.rotation.eulerAngles.x;
            initialTouchPosition = eventData.position;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isJoystickPressed = false;
        isRotating = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isJoystickPressed)
        {
            joystickPosition = eventData.position;

            Vector3 direction = new Vector3(joystickPosition.x - joystickBackground.position.x, joystickPosition.y - joystickBackground.position.y, 0);
            float distance = Vector2.Distance(joystickPosition, joystickBackground.position);

            if (distance < joystickBackground.rect.width / 2)
            {
                joystickHandle.anchoredPosition = direction * distance;
            }
            else
            {
                joystickHandle.anchoredPosition = direction * joystickBackground.rect.width / 2;
            }
        }
    }
}