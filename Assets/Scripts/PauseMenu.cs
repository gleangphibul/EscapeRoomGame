using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;

    void Start()
    {
        pauseMenuUI.SetActive(false);   
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightShift))
        {
            if (GameIsPaused)
            {
                Resume();
            } else
            {
                Pause();
            }
        }
    }

   public void Resume()
    {
        // Turn off Pause Menu UI and change time back to normal
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
        // Set the Pause Menu UI as active and freeze time
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
