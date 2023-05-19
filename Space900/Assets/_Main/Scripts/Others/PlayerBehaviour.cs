using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{

    [SerializeField] HB_Scripts _healthbar;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)){
            PlayerTakeDamage(20);
            Debug.Log(GameManager.gameManager._playerHealth.Health);
        }
        if(Input.GetKeyDown(KeyCode.LeftShift)){
            PlayerHeal(10);
            Debug.Log(GameManager.gameManager._playerHealth.Health);
        }
    }

    private void PlayerTakeDamage(int damage){

        GameManager.gameManager._playerHealth.DamageUnit(damage);
        _healthbar.setHealth(GameManager.gameManager._playerHealth.Health);
    }
    private void PlayerHeal(int healing){

        GameManager.gameManager._playerHealth.HealUnit(healing);
        _healthbar.setHealth(GameManager.gameManager._playerHealth.Health);

    }
}
