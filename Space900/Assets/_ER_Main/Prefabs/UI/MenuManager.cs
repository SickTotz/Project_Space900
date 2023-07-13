using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("ER_TestScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
