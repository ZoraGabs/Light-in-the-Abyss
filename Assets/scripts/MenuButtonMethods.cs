using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MenuButtonMethods : MonoBehaviour
{
    [SerializeField] Slider volumeMusic;
    [SerializeField] Slider volumeEffects;
    [SerializeField] TMP_Dropdown escolher_lingua;
    [SerializeField] Canvas maincanva;
    [SerializeField] TextMeshProUGUI press_start;
    [SerializeField] Canvas optionscanva;
    [SerializeField] TextMeshProUGUI english_options;
    [SerializeField] TextMeshProUGUI portuguese_options;

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

    void Start()
    {
        maincanva.enabled = false;
        optionscanva.enabled = false;
    }

    private IEnumerator FadingLetters()
    {
        Color originalColor = press_start.color;
        Color newColor = originalColor;
        float alphaReduc = 0.05f; 
        while (newColor.a > 0)
        {
            newColor.a -= alphaReduc;
            newColor.a = Mathf.Max(newColor.a, 0);
            press_start.color = newColor;
            Canvas.ForceUpdateCanvases();
            yield return new WaitForSeconds(0.02f); 
        }
        maincanva.enabled = true;
    }

    void Update()
    {
        if (press_start.enabled == true && Input.anyKey)
        {
            StartCoroutine(FadingLetters());
        }
    }

    public void NewGame()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadGame()
    {
        // depois ainda tenho que terminar o sistema de load
    }

    public void Options()
    {
        optionscanva.enabled = true;
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

    public void Quit()
    {
        Debug.Log("saiu fi");
        Application.Quit();
    }

    public void ReturnFromOptions()
    {
        optionscanva.enabled = false;
    }
}
