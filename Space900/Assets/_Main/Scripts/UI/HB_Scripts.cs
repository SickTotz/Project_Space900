using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HB_Scripts : MonoBehaviour
{
    Slider _healthSlider;

    private void Start() {

        _healthSlider = GetComponent<Slider>();
    }

    public void setMaxHealth(int maxHealth){

        _healthSlider.maxValue = maxHealth;
        _healthSlider.value = maxHealth;
    }

    public void setHealth(int health){

        _healthSlider.value = health;
    }
}
