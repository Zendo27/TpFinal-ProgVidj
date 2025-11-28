using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;   // necesario para IEnumerator

public class PanelManager : MonoBehaviour
{
    private GameObject gameOverPanel;
    private GameObject victoryPanel;

    void Awake()
    {
        gameOverPanel = GameObject.FindGameObjectWithTag("GameOverPanel");
        victoryPanel = GameObject.FindGameObjectWithTag("VictoryPanel");

        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);

        if (victoryPanel != null)
            victoryPanel.SetActive(false);
    }

    // ------------ GAME OVER INMEDIATO ------------
    public void TriggerGameOver()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    // ------------ GAME OVER CON DELAY ------------
    public void TriggerGameOverDelayed(float delay = 1.5f)
    {
        StartCoroutine(GameOverRoutine(delay));
    }

    private IEnumerator GameOverRoutine(float delay)
    {
        yield return new WaitForSeconds(delay);
        TriggerGameOver();
    }

    // ------------ VICTORY ------------
    public void TriggerVictory()
    {
        if (victoryPanel != null)
        {
            victoryPanel.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    // ------------ BOTONES ------------
    public void RetryLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MenuInicio");
    }

    public void NextLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
