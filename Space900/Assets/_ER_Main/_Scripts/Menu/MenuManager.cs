using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class MenuManager : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public GameObject menuUI;
    public string gameSceneName = "ER_TestScene";
    private bool isVideoFinished = false;
    public AudioManager audioManager; // Assegna manualmente l'oggetto AudioManager nell'editor Unity
    private bool isPlayingGame = false; // Variabile per tenere traccia dello stato di gioco

    private void Start()
    {
        videoPlayer.loopPointReached += OnVideoFinished;
        videoPlayer.Stop();
        audioManager.PlaySoundWithFade("MainMenu Theme", 1.0f); // Riproduci l'audio di sfondo con fade-in all'avvio del menu
    }

    public void PlayGame()
    {
        menuUI.SetActive(false);
        audioManager.StopSoundWithFade(1.0f); // Interrompi l'audio con fade-out
        videoPlayer.Play();
        isPlayingGame = true; // Imposta lo stato di gioco su "true" quando si avvia il gioco
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void Update()
    {
        if (isPlayingGame && isVideoFinished && !videoPlayer.isPlaying)
        {
            SceneManager.LoadScene(gameSceneName);
        }
    }

    private void OnVideoFinished(VideoPlayer vp)
    {
        isVideoFinished = true;

        if (isPlayingGame)
        {
            audioManager.PlaySoundWithFade("MainMenu Theme", 1.0f); // Riproduci nuovamente l'audio di sfondo quando il video è finito e si è tornati al menu di gioco
        }
    }

    private void OnEnable()
    {
        // Riproduci l'audio di sfondo del menu quando si torna al menu di gioco
        if (isPlayingGame)
        {
            menuUI.SetActive(true); // Riattiva il menu di gioco
            audioManager.PlaySoundWithFade("MainMenu Theme", 1.0f); // Riproduci l'audio di sfondo del menu con fade-in
        }
    }
}
