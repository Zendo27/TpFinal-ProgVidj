using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class OptionsMenu : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Slider volumenFX;
    [SerializeField] private Slider volumenMaster;
    [SerializeField] private Toggle mute;

    [Header("Audio")]
    [SerializeField] private AudioMixer mixer;

    private float lastVol = 0;

    void Start()
    {
        volumenFX.onValueChanged.AddListener(ChangeVolumenFX);
        volumenMaster.onValueChanged.AddListener(ChangeVolumenMaster);

        mixer.GetFloat("volMaster", out lastVol);
    }

    public void ChangeVolumenMaster(float v)
    {
        mixer.SetFloat("volMaster", v);
    }

    public void ChangeVolumenFX(float v)
    {
        mixer.SetFloat("volFX", v);
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

    public void CloseMenu()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1f;
    }
}
