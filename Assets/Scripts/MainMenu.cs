using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Classroom");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void WinGame()
    {
        SceneManager.LoadScene("Ending");
    }
}
