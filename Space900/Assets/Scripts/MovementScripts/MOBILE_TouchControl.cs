using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MOBILE_TouchControl : MonoBehaviour
{   
    // Creazione delle variabili per gli input touchscreen
    public float rightFingerID, leftFingerID;       // Variabili per dito sinistro e dito destro
    public float screenWidth;                       // Variabile per la definizione dello schermo di gioco
    public Vector2 lookInput;                       // Variabile vettore per la visuale

    public Vector2 moveTouchStartPostition;         // Variabile vettore: posizioneIniziale
    public Vector2 moveInput;                       // Variabile vettore: input di movimento
    public float sensitivity;                       // Variabile della sensibilita' della visuale

    // Start is called before the first frame update
    void Start()
    {
        leftFingerID = -1;      // Settaggio a -1 per gli input del movimento
        rightFingerID = -1;     // Settaggio a -1 per gli input della visuale
        screenWidth = Screen.width;
    }

    // Update is called once per frame
    void Update()
    {
        getTouch();     // Utilizzo della funzione getTouch per ottenere le informazioni dei comandi touch
    }

    void getTouch()
    {
        for(int i = 0; i < Input.touchCount; i++){
            
            Touch t = Input.GetTouch(i);            // Variabile touch, la descriviamo piu' avanti

            // Serie di switch...
            switch (t.phase){

                case TouchPhase.Began:              // Nel caso in cui il comincia l'input da touch...
                    if(t.position.x > (screenWidth / 2) && rightFingerID == -1){    // Se la posizione del dito > della grandezza del dello schermo / 2 e si trova a -1...
                        rightFingerID = t.fingerId;                                         // Il dito destro corrisponde ai comandi del dito destro stesso
                    }
                    else{
                        if(t.position.x < (screenWidth / 2) && leftFingerID == -1){
                            leftFingerID = t.fingerId;
                            moveTouchStartPostition = t.position;
                        }
                    }
                    break;                                                          // Interrompi
                
                case TouchPhase.Canceled:           // Nel caso in cui l'input da touch viene cancellato...
                case TouchPhase.Ended:              // o eliminato... 
                    if(t.fingerId == rightFingerID){        
                        rightFingerID = -1;
                    }
                    else{
                        if(t.fingerId == leftFingerID){
                            leftFingerID = -1;
                            moveTouchStartPostition = moveInput = Vector2.zero;
                        }
                    }
                    break;

                case TouchPhase.Moved:
                    if(rightFingerID == t.fingerId){
                        lookInput = t.deltaPosition * Time.deltaTime * sensitivity;
                    }
                    else{
                        if(leftFingerID == t.fingerId){
                            moveInput = t.position - moveTouchStartPostition;
                        }
                    }
                    break;
                
                case TouchPhase.Stationary:
                    lookInput = Vector2.zero;

                    break;
            }
        }
    }
}
