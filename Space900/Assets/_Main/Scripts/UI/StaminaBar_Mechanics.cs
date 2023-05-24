using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar_Mechanics : MonoBehaviour
{
    Slider _staminaSlider;

    private void Start() {

        _staminaSlider = GetComponent<Slider>();
    }

    public void setMaxStamina(float maxStamina){

        _staminaSlider.maxValue = maxStamina;
        _staminaSlider.value = maxStamina;
    }

    public void setStamina(float stamina){

        _staminaSlider.value = stamina;
    }
}