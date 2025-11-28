using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MainPanel : MonoBehaviour
{
    [Header("Option")]
    [SerializeField] private Slider volumenFX;
    [SerializeField] private Slider volumenMaster;
    [SerializeField] private Toggle mute;
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private AudioSource fxSource;
    [SerializeField] private AudioClip clickSound;
    private float lastVol;
    [Header("Panels")]
    [SerializeField] private GameObject mainPanel;
    [SerializeField] private GameObject optionPanel;

    
    private void Awake()
    {
        volumenFX.onValueChanged.AddListener(ChangeVolumenFX);
        volumenMaster.onValueChanged.AddListener(ChangeVolumenMaster);
    }
    
    public void SetMute() 
    {
        if (mute.isOn) 
        {
            mixer.GetFloat("volMaster", out lastVol);
            mixer.SetFloat("volMaster", -80);
        }
        else 
        {
            mixer.SetFloat("volMaster", lastVol);
        }
    }

    public void OpenPanel(GameObject panel) 
    {
         mainPanel.SetActive(false);
        optionPanel.SetActive(false);

        panel.SetActive(true);
        PlaySoundButton();
    }
    public void PlayGame()
    {
        SceneManager.LoadScene("LvL1");
    }

    public void ChangeVolumenMaster(float v) 
    {
        mixer.SetFloat("volMaster", v);
    }

    public void ChangeVolumenFX(float v)
    {
        mixer.SetFloat("volFX", v);
    }

    public void PlaySoundButton() 
    {
        fxSource.PlayOneShot(clickSound);
    }
}
