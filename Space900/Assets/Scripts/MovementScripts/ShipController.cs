using UnityEngine;

public class ShipController : MonoBehaviour
{
    //Velocità massima della navicella
    public float maxSpeed = 10f;
    
    //Accelerazione della navicella
    public float acceleration = 2f;
    
    //Rallentamento della navicella
    public float deceleration = 0.5f;
    
    //Angolo di rotazione massimo
    public float maxRotationAngle = 45f;
    
    //Variabili per la gestione del movimento
    private float currentSpeed = 0f;
    private Vector2 touchStartPos = Vector2.zero;
    private float touchDelta = 0f;
    
    //Variabili per la gestione della rotazione
    private float currentRotationAngle = 0f;
    private Vector2 rotationStartPos = Vector2.zero;
    private float rotationDelta = 0f;

    void Update()
    {
        //Movimento della navicella
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchPos = new Vector2(touch.position.y, Screen.width - touch.position.x);
            
            if (touchPos.x < Screen.width / 2)
            {
                if (touch.phase == TouchPhase.Began)
                {
                    touchStartPos = touchPos;
                    currentSpeed = 0f;
                }
                else if (touch.phase == TouchPhase.Moved)
                {
                    touchDelta = touchPos.y - touchStartPos.y;
                    float speed = Mathf.Clamp(touchDelta / Screen.height * maxSpeed, 0f, maxSpeed);
                    currentSpeed = Mathf.MoveTowards(currentSpeed, speed, acceleration * Time.deltaTime);
                    transform.Translate(0f, 0f, currentSpeed * Time.deltaTime);
                }
                else if (touch.phase == TouchPhase.Ended)
                {
                    touchStartPos = Vector2.zero;
                    currentSpeed = Mathf.MoveTowards(currentSpeed, 0f, deceleration * Time.deltaTime);
                    transform.Translate(0f, 0f, currentSpeed * Time.deltaTime);
                }
            }
            
            //Rotazione della navicella
            else if (touchPos.x >= Screen.width / 2)
            {
                if (touch.phase == TouchPhase.Began)
                {
                    rotationStartPos = touchPos;
                    currentRotationAngle = transform.eulerAngles.y;
                }
                else if (touch.phase == TouchPhase.Moved)
                {
                    rotationDelta = touchPos.x - rotationStartPos.x;
                    float angle = Mathf.Clamp(rotationDelta / Screen.width * maxRotationAngle, -maxRotationAngle, maxRotationAngle);
                    transform.eulerAngles = new Vector3(transform.eulerAngles.x, currentRotationAngle + angle, transform.eulerAngles.z);
                }
                else if (touch.phase == TouchPhase.Ended)
                {
                    rotationStartPos = Vector2.zero;
                }
            }
        }
        else
        {
            currentSpeed = Mathf.MoveTowards(currentSpeed, 0f, deceleration * Time.deltaTime);
            transform.Translate(0f, 0f, currentSpeed * Time.deltaTime);
        }
    }
}
