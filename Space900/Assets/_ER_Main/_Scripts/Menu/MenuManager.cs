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

    private void Start()
    {
        videoPlayer.loopPointReached += OnVideoFinished;
        videoPlayer.Stop();
    }

    public void PlayGame()
    {
        menuUI.SetActive(false);
        videoPlayer.Play();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void Update()
    {
        if (isVideoFinished && !videoPlayer.isPlaying)
        {
            SceneManager.LoadScene(gameSceneName);
        }
    }

    private void OnVideoFinished(VideoPlayer vp)
    {
        isVideoFinished = true;
    }
}
