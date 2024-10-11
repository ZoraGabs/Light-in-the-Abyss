using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Buttons_Pause : MonoBehaviour
{
    public GameObject english_options;
    public GameObject portuguese_options;
    public GameObject Options_Canvas;
    [SerializeField] TMP_Dropdown escolher_lingua;
    [SerializeField] Slider volumeMusic;
    [SerializeField] Slider volumeEffects;

    public void Continue()
    {
        Time.timeScale = 1.0f;
        gameObject.SetActive(false);
    }

    public void Options()
    {
        Options_Canvas.SetActive(true);
        if (PlayerPrefs.HasKey("Language"))
        {
            if (PlayerPrefs.GetInt("Language") == 1)
            {
                english_options.gameObject.SetActive(true);
                portuguese_options.gameObject.SetActive(false);
            }
            else if (PlayerPrefs.GetInt("Language") == 0)
            {
                portuguese_options.gameObject.SetActive(true);
                english_options.gameObject.SetActive(false);
            }
        }
        else
        {
            PlayerPrefs.SetInt("Language", 0);
            english_options.gameObject.SetActive(true);
        }
    }

    public void OnOptionChanged()
    {
        switch (escolher_lingua.value)
        {
            case 0:
                PlayerPrefs.SetInt("Language", 0);
                english_options.gameObject.SetActive(false);
                portuguese_options.gameObject.SetActive(true);
                //colocar player prefs e mudar visibilidade dos textos
                break;
            case 1:
                PlayerPrefs.SetInt("Language", 1);
                english_options.gameObject.SetActive(true);
                portuguese_options.gameObject.SetActive(false);
                //colocar player prefs e mudar visibilidade dos textos
                break;
        }
    }

    public void Exit()
    {
        SceneManager.LoadScene(0);
    }
    public void Return()
    {
        Options_Canvas.gameObject.SetActive(false);
    }

    public void OnVolumeSliderChanged()
    {
        float volumeM = volumeMusic.value;

        PlayerPrefs.SetFloat("VolumeMusic", volumeM);
        //depois fazer isso ser aplicado no som.
    }

    public void OnVolumeSliderChangedEffects()
    {
        float volumeE = volumeEffects.value;

        PlayerPrefs.SetFloat("VolumeEffects", volumeE);
        //depois fazer isso ser aplicado no som.
    }
}
