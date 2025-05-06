using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] float remainingTime = 20;
   
    private bool timeIsUp = false;


    void Update()
    {
        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
        }
        else if (!timeIsUp)
        {
            remainingTime = 0;
            timeIsUp = true;
            timerText.color = Color.red;
            Invoke("LoadGameOverScene", 1f);
        }

        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void LoadGameOverScene()
    {
        SceneManager.LoadScene("Game Over");
    }
}
