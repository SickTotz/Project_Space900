/*
// Versione Originale
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{

    [SerializeField] HealthBar_Mechanics _healthbar;
    [SerializeField] StaminaBar_Mechanics _staminaBar;
    [SerializeField] MOBILE_MovementMechanics _playerController;
    float _playerOriginalSpeed;
    float _playerSprintSpeed;


    // Start is called before the first frame update
    void Start()
    {
        _playerOriginalSpeed = _playerController.forwardSpeed;
        _playerSprintSpeed = _playerController.forwardSpeed * 2;
    }

    // Update is called once per frame
    public void Update()
    {   
        // DEBUG HEALTH BAR AND STAMINA BAR

        // Health input
        if(Input.GetKeyDown(KeyCode.Space)){
            PlayerTakeDamage(20);
        }
        if(Input.GetKeyDown(KeyCode.Q)){
            PlayerHeal(10);
        }

        // Stamina input
        if(Input.GetKey(KeyCode.LeftShift)){
            if(GameManager.gameManager._playerStamina.Stamina > 0){
                PlayerUseStamina(60f);
                if(_playerController.forwardSpeed != _playerSprintSpeed){
                    _playerController.forwardSpeed = _playerSprintSpeed;
                }
            }
            else{
                _playerController.forwardSpeed = _playerOriginalSpeed;
            }
        }
        else{
            PlayerRegenStamina();
            if(_playerController.forwardSpeed != _playerOriginalSpeed){
                _playerController.forwardSpeed = _playerOriginalSpeed;
            }
        }
    }

    public void PlayerTakeDamage(int damage){

        GameManager.gameManager._playerHealth.DamageUnit(damage);
        _healthbar.setHealth(GameManager.gameManager._playerHealth.Health);
    }
    public void PlayerHeal(int healing){

        GameManager.gameManager._playerHealth.HealUnit(healing);
        _healthbar.setHealth(GameManager.gameManager._playerHealth.Health);

    }
    public void PlayerUseStamina(float staminaAmount){

        GameManager.gameManager._playerStamina.UseStamina(staminaAmount);
        _staminaBar.setStamina(GameManager.gameManager._playerStamina.Stamina);
    }
    public void PlayerRegenStamina(){

        GameManager.gameManager._playerStamina.RegenStamina();
        _staminaBar.setStamina(GameManager.gameManager._playerStamina.Stamina);
    }
}
*/

// Altra Versione ( gestione da input con un bottone su schermo )
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] HealthBar_Mechanics _healthbar;
    [SerializeField] StaminaBar_Mechanics _staminaBar;
    [SerializeField] MOBILE_MovementMechanics _playerController;
    float _playerOriginalSpeed;
    float _playerSprintSpeed;
    private bool isButtonPressed = false;


    // Start is called before the first frame update
    void Start()
    {
        _playerOriginalSpeed = _playerController.forwardSpeed;
        _playerSprintSpeed = _playerController.forwardSpeed * 2;
    }

    // Update is called once per frame
    public void Update()
    {
        /* DEBUG HEALTH BAR AND STAMINA BAR */

        // Health input
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayerTakeDamage(20);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            PlayerHeal(10);
        }

        // Stamina input
        if (isButtonPressed)
        {
            if (GameManager.gameManager._playerStamina.Stamina > 0)
            {
                PlayerUseStamina(60f);
                if (_playerController.forwardSpeed != _playerSprintSpeed)
                {
                    _playerController.forwardSpeed = _playerSprintSpeed;
                }
            }
            else
            {
                _playerController.forwardSpeed = _playerOriginalSpeed;
            }
        }
        else
        {
            PlayerRegenStamina();
            if (_playerController.forwardSpeed != _playerOriginalSpeed)
            {
                _playerController.forwardSpeed = _playerOriginalSpeed;
            }
        }
    }

    public void PlayerTakeDamage(int damage)
    {
        GameManager.gameManager._playerHealth.DamageUnit(damage);
        _healthbar.setHealth(GameManager.gameManager._playerHealth.Health);
    }

    public void PlayerHeal(int healing)
    {
        GameManager.gameManager._playerHealth.HealUnit(healing);
        _healthbar.setHealth(GameManager.gameManager._playerHealth.Health);
    }

    public void PlayerUseStamina(float staminaAmount)
    {
        GameManager.gameManager._playerStamina.UseStamina(staminaAmount);
        _staminaBar.setStamina(GameManager.gameManager._playerStamina.Stamina);
    }

    public void PlayerRegenStamina()
    {
        GameManager.gameManager._playerStamina.RegenStamina();
        _staminaBar.setStamina(GameManager.gameManager._playerStamina.Stamina);
    }

    public void OnStaminaButtonPressed(bool pressed)
    {
        isButtonPressed = pressed;

        if (isButtonPressed && GameManager.gameManager._playerStamina.Stamina > 0)
        {
            PlayerUseStamina(60f);
            if (_playerController.forwardSpeed != _playerSprintSpeed)
            {
                _playerController.forwardSpeed = _playerSprintSpeed;
            }
        }
        else
        {
            _playerController.forwardSpeed = _playerOriginalSpeed;
        }
    }
}
