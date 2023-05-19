using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager { get; private set; }
    public Unit_Health _playerHealth = new Unit_Health(100, 100);   // e` possibile utilizzarlo su altri script per altri personaggi o ogetti, 
                                                                    // basta copiarla all'interno dello script del personaggio o ogetto in considerazione

    void Awake()
    {
        if(gameManager != null && gameManager != this){
            Destroy(this);
        }
        else{
            gameManager = this;
        }
    }
}
