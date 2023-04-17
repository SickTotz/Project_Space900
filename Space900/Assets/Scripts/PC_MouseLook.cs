using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PC_MouseLook : MonoBehaviour
{   
    // Creazione di ulteriori variabili per provare nuovi input del mouse
    
    public float sensitivity = 100f, sensitivityMultiplier;
    public Transform parentBody;
    public float velocity, maxUpwardRotation;
    float _mouseX, _mouseY, _xRotation, _yRotation;
    public MOBILE_TouchControl touch;           // Link delle meccaniche di touch

    // Start is called before the first frame update
    void Start()
    {   

    }

    // Update is called once per frame
    void Update()
    {   
        if(touch.rightFingerID != -1){
            Look();
        }
    }
    void Look(){

        _mouseX = touch.lookInput.x;
        _mouseY = touch.lookInput.y;
        _xRotation += _mouseX * sensitivity * Time.deltaTime;
        _yRotation += _mouseY * sensitivity * sensitivityMultiplier * Time.deltaTime;
        _yRotation = Mathf.Clamp(_yRotation, -maxUpwardRotation, maxUpwardRotation);


        parentBody.transform.rotation = Quaternion.Euler(0, _xRotation, 0);
        transform.localRotation = Quaternion.Euler(_yRotation, 0, 0);
    }
}
