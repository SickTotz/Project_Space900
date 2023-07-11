using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_Health
{
    // Fields
    int _currentHealth;
    int _currentMaxHealth;

    // Proprieties
    public int Health {
        get{
            return _currentHealth;
        }
        set{
            _currentHealth = value;
        }
    }

    public int MaxHealth {
        get{
            return _currentMaxHealth;
        }
        set{
            _currentMaxHealth = value;
        }
    }

    // Constructor 
    public Unit_Health(int health, int  maxHealth){

        _currentHealth = health;
        _currentMaxHealth = maxHealth;
    }

    // Methods
    public void DamageUnit(int damageAmount){

        if(_currentHealth > 0){
            _currentHealth -= damageAmount;
        }
    }

    public void HealUnit(int healAmount){

        if(_currentHealth < _currentMaxHealth){
            _currentHealth += healAmount;
        }
        if(_currentHealth > _currentMaxHealth){
            _currentHealth = _currentMaxHealth;
        }
    }
}
