using UnityEngine;
using UnityEngine.UI;

public class Touch_MovementMechanics : MonoBehaviour
{
    // Creazione di variabili per il movimento del player
    public float forwardSpeed = 25f, strafeSpeed = 7.5f, hoverSpeed = 5f;
    private float activeForwardSpeed, activeStrafeSpeed, activeHoverSpeed;
    private float forwardAcceleration = 2.5f, strafeAcceleration = 2f, hoverAcceleration = 2f;

    public float lookRateSpeed = 90f;
    private Vector2 lookInput, screenCenter, mouseDistance;

    private float rollInput;
    public float rollSpeed = 90f, rollAcceleration = 3.5f;

    // Aggiunta di un joystick virtuale per il movimento del personaggio
    public Image joystickBackground;
    public Image joystick;

    // Start is called before the first frame update
    void Start()
    {
        // Variabili per la definizione della visuale in gioco
        screenCenter.x = Screen.width * .5f;
        screenCenter.y = Screen.height * .5f;
    }

    // Update is called once per frame
    void Update()
    {
        // Impostazioni per la visualizzazione con il joystick
        Vector2 input = joystick.transform.position - joystickBackground.transform.position;
        input /= joystickBackground.rectTransform.sizeDelta.x * 0.5f;

        // Impostazioni per la rotazione del personaggio con il touch
        lookInput.x = input.x * screenCenter.y;
        lookInput.y = input.y * screenCenter.y;

        // Setting della velocita` massima del movimento da input col joystick
        rollInput = Mathf.Lerp(rollInput, Input.GetAxisRaw("Roll"), rollAcceleration * Time.deltaTime);

        transform.Rotate(-lookInput.y * lookRateSpeed * Time.deltaTime, lookInput.x * lookRateSpeed * Time.deltaTime, rollInput * rollSpeed * Time.deltaTime, Space.Self);

        // Utilizzo delle variabili per i movimenti del player
        activeForwardSpeed = Mathf.Lerp(activeForwardSpeed, input.y * forwardSpeed, forwardAcceleration * Time.deltaTime);
        activeStrafeSpeed = Mathf.Lerp(activeStrafeSpeed, input.x * strafeSpeed, strafeAcceleration * Time.deltaTime);
        activeHoverSpeed = Mathf.Lerp(activeHoverSpeed, Input.GetAxisRaw("Hover") * hoverSpeed, hoverAcceleration * Time.deltaTime);

        // Attribuizione del movimento al player
        transform.position += transform.forward * activeForwardSpeed * Time.deltaTime;
        transform.position += transform.right * activeStrafeSpeed * Time.deltaTime;
        transform.position += transform.up * activeHoverSpeed * Time.deltaTime;
    }
}
