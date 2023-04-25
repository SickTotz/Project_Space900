using UnityEngine;
using UnityEngine.UI;

public class Prova_VirtualJoystickController : MonoBehaviour
{
    [SerializeField] private Image joystickImage;
    [SerializeField] private float joystickRadius = 50f;

    private Vector2 joystickStartPosition;
    private bool joystickActive;
    private int touchId;
    private Prova_ShipMovementController shipMovementController;

    private void Start()
    {
        joystickStartPosition = joystickImage.rectTransform.position;
        shipMovementController = GameObject.Find("P_Navicella").GetComponent<Prova_ShipMovementController>();
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

                shipMovementController.SetMoveDirection(new Vector3(x, 0f, z));
            }

            if (touch.phase == TouchPhase.Ended && joystickActive && touch.fingerId == touchId)
            {
                joystickActive = false;
                joystickImage.rectTransform.position = joystickStartPosition;
                shipMovementController.StopMoving();
            }
            else
            {
                joystickActive = false;
                shipMovementController.StopMoving();
            }
        }
    }
}