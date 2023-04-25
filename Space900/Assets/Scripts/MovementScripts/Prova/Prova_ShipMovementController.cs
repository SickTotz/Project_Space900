using UnityEngine;

public class Prova_ShipMovementController : MonoBehaviour
{
    [SerializeField] private float maxSpeed = 30f; // valore da impostare che permette alla navicella di potersi muovere velocemente ma non troppo -> consigliato tra 10 e 30
    [SerializeField] private float acceleration = 10f; // valore da impostare che permette alla navicella di accelerare e raggiungere la sua velocita massima -> consigliato tra 5 e 15
    [SerializeField] private float deceleration = 3f; // valore da impostare che permette alla navicella di rallentare guardualmente -> consigliato tra 1 e 5
    [SerializeField] private float decelerationDelay = 0.5f; // valore da impostare che esegue un ritardo prima di rallentare la navicella -> consigliato tra 0.2 a 0.5

    private Vector3 moveDirection;
    private float currentSpeed;
    private float decelerationTimer;

    private void Update()
    {
        if (moveDirection != Vector3.zero)
        {
            currentSpeed = Mathf.MoveTowards(currentSpeed, maxSpeed, acceleration * Time.deltaTime);
        }
        else if (decelerationTimer > 0f)
        {
            decelerationTimer -= Time.deltaTime;
        }
        else
        {
            currentSpeed = Mathf.MoveTowards(currentSpeed, 0f, deceleration * Time.deltaTime);
        }

        transform.Translate(moveDirection * currentSpeed * Time.deltaTime, Space.World);
    }

    public void SetMoveDirection(Vector3 direction)
    {
        moveDirection = direction;
        moveDirection = Vector3.ClampMagnitude(moveDirection, 1f);
        decelerationTimer = 0f;
    }

    public void StopMoving()
    {
        moveDirection = Vector3.zero;
        decelerationTimer = decelerationDelay;
    }
}
