using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ER_PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float maxMovementRadius = 5f;
    public float maxDistanceFromOriginY = 5f;

    private bool isDragging = false;
    private Vector2 dragStartPosition;
    private Vector3 originalPosition;
    private Vector3 lastValidPosition;

    private bool canPlayerInput = true; // Aggiunta della variabile per controllare l'input del giocatore

    private void Start()
    {
        originalPosition = transform.position;
        lastValidPosition = originalPosition;
    }

    private void Update()
    {
        // Movimento automatico lungo l'asse Z
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        // Controllo del input da touchscreen
        if (canPlayerInput)
        {
            HandleTouchInput();
        }
    }

    private void HandleTouchInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                isDragging = true;
                dragStartPosition = touch.position;
            }

            if (touch.phase == TouchPhase.Ended)
            {
                isDragging = false;
                lastValidPosition = transform.position;
            }

            if (isDragging)
            {
                Vector2 currentDragPosition = touch.position;
                Vector2 dragDelta = currentDragPosition - dragStartPosition;

                // Calcolo dello spostamento orizzontale e verticale
                float horizontalMovement = dragDelta.x * 0.01f;
                float verticalMovement = dragDelta.y * 0.01f;

                // Calcolo della nuova posizione
                Vector3 newPosition = lastValidPosition + new Vector3(horizontalMovement, verticalMovement, 0f);

                // Limitazione del movimento all'interno del raggio massimo
                float clampedX = Mathf.Clamp(newPosition.x, originalPosition.x - maxMovementRadius, originalPosition.x + maxMovementRadius);
                float clampedY = Mathf.Clamp(newPosition.y, originalPosition.y, originalPosition.y + maxDistanceFromOriginY); // Impedisce di scendere al di sotto del limite minimo iniziale
                newPosition = new Vector3(clampedX, clampedY, transform.position.z);

                // Applica la nuova posizione alla navicella
                transform.position = newPosition;
            }
        }
    }

    // Metodo per abilitare o disabilitare l'input del giocatore
    public void SetPlayerInputEnabled(bool enabled)
    {
        canPlayerInput = enabled;
    }
}
