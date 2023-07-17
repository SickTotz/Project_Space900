using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public bool IsGameRestarting = false;
    public GameObject PauseMenuUI;
    public ER_PlayerController playerController;

    private BlasterMechanics blasterMechanics;

    private void Start()
    {
        playerController = FindObjectOfType<ER_PlayerController>();
        blasterMechanics = FindObjectOfType<BlasterMechanics>();
    }

    public void PauseGame()
    {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        playerController.SetPlayerInputEnabled(false); // Disabilita l'input del giocatore durante la pausa
        blasterMechanics.enabled = false; // Disabilita il componente BlasterMechanics durante la pausa
    } 

    public void ResumeGame()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        playerController.SetPlayerInputEnabled(true); // Riabilita l'input del giocatore quando il gioco riprende
        blasterMechanics.enabled = true; // Riabilita il componente BlasterMechanics quando il gioco riprende
    }

    public void RestartGame()
    {
        IsGameRestarting = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void ExitToMenu()
    {   
        SceneManager.LoadScene("MainMenu");
    }
}
