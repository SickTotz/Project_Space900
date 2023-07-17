using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] private int minHealth = 0; // Vita minima per il game over

    public GameObject GameOverUI;
    private PlayerBehaviour playerBehaviour;
    public ER_PlayerController playerController;

    public bool isGameOver = false;

    private void Start()
    {
        playerBehaviour = FindObjectOfType<PlayerBehaviour>();

        playerController = FindObjectOfType<ER_PlayerController>();

        if (playerBehaviour == null)
        {
            Debug.LogError("PlayerBehaviour not found in the scene!");
        }

        playerBehaviour.PlayerTakeDamageEvent += CheckGameOver;
    }

    private void OnDisable()
    {
        playerBehaviour.PlayerTakeDamageEvent -= CheckGameOver;
    }

    private void CheckGameOver(int currentHealth)
    {
        if (!isGameOver && currentHealth <= minHealth)
        {
            isGameOver = true;
            GameOver();
        }
    }

    private void GameOver()
    {
        // Ferma il gioco
        Time.timeScale = 0f;
        Debug.Log("G A M E  O V E R");
        GameOverUI.SetActive(true);
        playerController.SetPlayerInputEnabled(false);
    }
}
