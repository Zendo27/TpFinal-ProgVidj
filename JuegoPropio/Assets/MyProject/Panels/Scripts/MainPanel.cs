using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MainPanel : MonoBehaviour
{
    [Header("Option")]
    [SerializeField] private Slider volumenFX;
    [SerializeField] private Slider volumenMaster;
    [SerializeField] private Toggle mute;
    [Header("Panels")]
    [SerializeField] private GameObject mainPanel;
    [SerializeField] private GameObject optionPanel;


    public void OpenPanel(GameObject panel) 
    {
         mainPanel.SetActive(false);
        optionPanel.SetActive(false);

        panel.SetActive(true);
    }
    public void PlayGame()
    {
        SceneManager.LoadScene("LvL1");
    }

}
