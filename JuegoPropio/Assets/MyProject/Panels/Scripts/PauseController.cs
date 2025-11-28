using UnityEngine;

public class PauseController : MonoBehaviour
{
    [SerializeField] private GameObject optionPanel;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            TogglePause();
    }

    public void TogglePause()
    {
        if (optionPanel.activeSelf)
        {
            NoPause();
        }
        else
        {
            Pause();
        }
    }

    public void NoPause() 
    {
        optionPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Pause() 
    {
        optionPanel.SetActive(true);
        Time.timeScale = 0f;
    }

}
