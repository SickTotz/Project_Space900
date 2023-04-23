using UnityEngine;

public class Touch_MovementMechanics : MonoBehaviour {
public float maxSpeed = 5.0f; // velocità massima della navicella
public float acceleration = 0.3f; // accelerazione della navicella
private float currentSpeed = 0.0f; // velocità attuale della navicella
private Vector2 touchStartPosition; // posizione iniziale del touch
private Vector2 touchEndPosition; // posizione finale del touch
private Vector2 touchDeltaPosition; // variazione tra la posizione iniziale e quella finale del touch
private bool isMoving = false; // indica se la navicella è in movimento
private bool isDecelerating = false; // indica se la navicella sta rallentando
private float decelerationRate = 0.1f; // tasso di diminuzione della velocità
private float smoothTime = 0.1f; // tempo impiegato da SmoothDamp per interpolare la velocità corrente verso lo zero
private float smoothVelocity = 0.0f; // velocità di interpolazione di SmoothDamp
public float sensitivity = 1.0f; // sensibilità del touch
public float dragThreshold = 5.0f; // soglia per considerare il movimento come drag
private bool isDragging = false; // indica se il movimento è un drag

    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).position.x < Screen.width / 2)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    touchStartPosition = touch.position;
                    isMoving = true;
                    break;

                case TouchPhase.Moved:
                    touchDeltaPosition = touch.position - touchStartPosition;

                    // controlla se il movimento è un drag o uno swipe
                    if (touchDeltaPosition.magnitude > dragThreshold)
                    {
                        isDragging = true;
                    }

                    if (isDragging)
                    {
                        // muove la navicella in base alla variazione di posizione del touch, con una sensibilità personalizzabile
                        transform.Translate(new Vector3(touchDeltaPosition.x, 0, touchDeltaPosition.y) * Time.deltaTime * currentSpeed * sensitivity, Space.Self);
                    }
                    else
                    {
                        // muove la navicella in base alla direzione del movimento del touch, con una sensibilità personalizzabile
                        if (touchDeltaPosition.y > 0)
                        {
                            transform.Translate(Vector3.forward * touchDeltaPosition.y * currentSpeed * Time.deltaTime * sensitivity, Space.Self);
                        }
                        else if (touchDeltaPosition.y < 0)
                        {
                            transform.Translate(Vector3.back * -touchDeltaPosition.y * currentSpeed * Time.deltaTime * sensitivity, Space.Self);
                        }

                        if (touchDeltaPosition.x > 0)
                        {
                            transform.Translate(Vector3.right * touchDeltaPosition.x * currentSpeed * Time.deltaTime * sensitivity, Space.Self);
                        }
                        else if (touchDeltaPosition.x < 0)
                        {
                            transform.Translate(Vector3.left * -touchDeltaPosition.x * currentSpeed * Time.deltaTime * sensitivity, Space.Self);
                        }
                    }

                    currentSpeed = Mathf.Clamp(currentSpeed + acceleration * Time.deltaTime, 0, maxSpeed); // aumenta la velocità attuale della navicella in base all'accelerazione definita, fino ad un massimo di velocità massima
                break;

            case TouchPhase.Ended:
                isMoving = false;

                if (!isDragging)
                {
                    // determina la direzione del movimento del touch
                    touchEndPosition = touch.position;
                    touchDeltaPosition = touchEndPosition - touchStartPosition;

                    // muove la navicella in base alla direzione del movimento del touch, con una sensibilità personalizzabile
                    if (touchDeltaPosition.y > 0)
                    {
                        transform.Translate(Vector3.forward * touchDeltaPosition.y * currentSpeed * Time.deltaTime * sensitivity, Space.Self);
                    }
                    else if (touchDeltaPosition.y < 0)
                    {
                        transform.Translate(Vector3.back * -touchDeltaPosition.y * currentSpeed * Time.deltaTime * sensitivity, Space.Self);
                    }

                    if (touchDeltaPosition.x > 0)
                    {
                        transform.Translate(Vector3.right * touchDeltaPosition.x * currentSpeed * Time.deltaTime * sensitivity, Space.Self);
                    }
                    else if (touchDeltaPosition.x < 0)
                    {
                        transform.Translate(Vector3.left * -touchDeltaPosition.x * currentSpeed * Time.deltaTime * sensitivity, Space.Self);
                    }
                }

                // resetta le variabili per la gestione del touch
                isDragging = false;
                touchStartPosition = Vector2.zero;
                touchEndPosition = Vector2.zero;
                touchDeltaPosition = Vector2.zero;

                break;
            }

            // se la navicella non sta più muovendo il player, diminuisce la velocità attuale verso lo zero con una interpolazione SmoothDamp
            if (!isMoving && currentSpeed > 0)
            {
                currentSpeed = Mathf.SmoothDamp(currentSpeed, 0, ref smoothVelocity, smoothTime, Mathf.Infinity, Time.deltaTime);

                // se la velocità attuale è molto vicina allo zero, la resetta a zero per evitare errori di arrotondamento
                if (currentSpeed < 0.01f)
                {
                    currentSpeed = 0;
                }
            }

            // se la navicella sta rallentando, diminuisce la velocità attuale in base al tasso di diminuzione della velocità definito
            if (isDecelerating && currentSpeed > 0)
            {
                currentSpeed -= decelerationRate * Time.deltaTime;

                // se la velocità attuale è molto vicina allo zero, la resetta a zero per evitare errori di arrotondamento
                if (currentSpeed < 0.01f)
                {
                    currentSpeed = 0;
                    isDecelerating = false;
                }
            }
        }    
    }
    // metodo per far rallentare la navicella gradualmente fino ad uno stop
    public void Decelerate()
    {
        isDecelerating = true;
    }
}
