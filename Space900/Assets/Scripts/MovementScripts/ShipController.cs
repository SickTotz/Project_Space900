using UnityEngine;
using UnityEngine.UI;

public class ShipController : MonoBehaviour
{
    public float maxSpeed = 15f; // velocità massima di movimento della navicella
    public float acceleration = 15f; // accelerazione della navicella
    public float deceleration = 15f; // decelerazione della navicella
    public Image joystickImage; // riferimento all'immagine del joystick UI
    public float joystickRadius = 50f; // raggio massimo del joystick

    private Vector3 moveDirection; // direzione di movimento della navicella
    private Vector2 joystickStartPosition; // posizione iniziale del joystick
    private bool joystickActive; // indica se il joystick è attivo
    private int touchId; // ID del tocco sullo schermo associato al joystick
    private float currentSpeed; // velocità corrente della navicella

    private void Start()
    {
        joystickStartPosition = joystickImage.rectTransform.position; // memorizza la posizione iniziale del joystick
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began && !joystickActive)
            {
                if (RectTransformUtility.RectangleContainsScreenPoint(joystickImage.rectTransform, touch.position))
                {
                    joystickActive = true; // il joystick è stato attivato
                    touchId = touch.fingerId; // memorizza l'ID del tocco sullo schermo
                }
            }

            if (touch.phase == TouchPhase.Moved && joystickActive && touch.fingerId == touchId)
            {
                Vector2 joystickPosition = touch.position - joystickStartPosition; // calcola la posizione del joystick rispetto alla posizione iniziale
                float distanceFromCenter = joystickPosition.magnitude; // calcola la distanza del joystick dal centro

                if (distanceFromCenter > joystickRadius)
                {
                    joystickPosition = joystickPosition.normalized * joystickRadius; // limita la posizione del joystick al raggio massimo
                }

                joystickImage.rectTransform.position = joystickStartPosition + joystickPosition; // sposta il joystick alla nuova posizione

                float x = joystickPosition.x / joystickRadius; // calcola l'input sull'asse x
                float z = joystickPosition.y / joystickRadius; // calcola l'input sull'asse z

                Vector3 newDirection = new Vector3(x, 0f, z); // crea una nuova direzione di movimento

                if (moveDirection.magnitude > 0f && Vector3.Dot(newDirection, moveDirection) < 0f)
                {
                    // Se la navicella sta già andando in una certa direzione e la nuova direzione
                    // è opposta, rallenta gradualmente la navicella fino a fermarla, poi cambia direzione.
                    currentSpeed -= deceleration * Time.deltaTime;
                    if (currentSpeed <= 0f)
                    {
                        moveDirection = newDirection;
                        currentSpeed = 0f;
                    }
                }
                else
                {
                    moveDirection = newDirection;
                    moveDirection = Vector3.ClampMagnitude(moveDirection, 1f);
                }
            }

            if (touch.phase == TouchPhase.Ended && joystickActive && touch.fingerId == touchId)
            {
                joystickActive = false; // il joystick è stato disattivato
                joystickImage.rectTransform.position = joystickStartPosition; // riposiziona il joystick nella posizione iniziale

                // Imposta la direzione di movimento a zero e diminuisci gradualmente la velocità
                moveDirection = Vector3.zero;
                currentSpeed -= deceleration * Time.deltaTime;

                if (currentSpeed < 0f)
                {
                    currentSpeed = 0f;
                }
            }

            if (moveDirection.magnitude > 0f)
            {
                currentSpeed += acceleration * Time.deltaTime; // aumenta la velocità della navicella
                currentSpeed = Mathf.Clamp(currentSpeed, 0f, maxSpeed); // limita la velocità massima della navicella

                transform.Translate(moveDirection * currentSpeed * Time.deltaTime, Space.World); // muove la navicella

                if (!joystickActive)
                {
                    currentSpeed -= deceleration * Time.deltaTime;  // diminuisce gradualmente la velocita` della navicella
                    if (currentSpeed < 0f)
                    {
                        currentSpeed = 0f; // assicura che la velocità non diventi negativa
                        moveDirection = Vector3.zero; // ferma la navicella
                    }
                }
            }
            else
            {
                currentSpeed -= deceleration * Time.deltaTime; // diminuisce gradualmente la velocità della navicella

                if (currentSpeed < 0f)
                {
                    currentSpeed = 0f; // assicura che la velocità non diventi negativa
                }
            }
        }
    }
}