using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;

public class OptionsMenuManager : MonoBehaviour
{
    public GameObject optionsPanel;
    public TMP_Dropdown controlSchemeDropdown;
    public Slider musicVolumeSlider;
    public Slider sfxVolumeSlider;
    public Button applyButton;
    public Button backButton;
    public TMP_Text savedMessage;
    public AudioMixer audioMixer; 
    public AudioSource audioSource;
    public AudioClip clickSound;

    private string controlScheme = "DFJK"; 
    private float musicVolume = 1f; 
    private float sfxVolume = 1f;

    void Start()
    {
        optionsPanel.SetActive(false); 
        savedMessage.gameObject.SetActive(false);

        controlSchemeDropdown.onValueChanged.AddListener(SetControlScheme);
        musicVolumeSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxVolumeSlider.onValueChanged.AddListener(SetSFXVolume);
        applyButton.onClick.AddListener(ApplySettings);
        backButton.onClick.AddListener(BackToMainMenu);

        InitializeUI();
    }

    public void ShowOptions()
    {
        optionsPanel.SetActive(true);
        savedMessage.gameObject.SetActive(false);
    }

    public void SetControlScheme(int index)
    {
        savedMessage.gameObject.SetActive(false);
        switch (index)
        {
            case 0:
                Debug.Log("Control scheme set to DFJK");
                controlScheme = "DFJK";
                break;
            case 1:
                Debug.Log("Control scheme set to Arrows");
                controlScheme = "Arrows";
                break;
            default:
                Debug.LogWarning("Unknown control scheme selected.");
                break;
        }
    }

    public void SetMusicVolume(float volume)
    {
        savedMessage.gameObject.SetActive(false);
        musicVolume = volume;
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20); 
    }

    public void SetSFXVolume(float volume)
    {
        savedMessage.gameObject.SetActive(false);
        sfxVolume = volume;
        audioMixer.SetFloat("SFXVolume", Mathf.Log10(volume) * 20); 
    }

    public void ApplySettings()
    {
        PlayClickSound();
        
        PlayerPrefs.SetString("ControlScheme", controlScheme);

        PlayerPrefs.SetFloat("MusicVolume", musicVolume);
        PlayerPrefs.SetFloat("SFXVolume", sfxVolume);

        PlayerPrefs.Save();
        savedMessage.gameObject.SetActive(true);
    }

    public void BackToMainMenu()
    {
        PlayClickSound();
        optionsPanel.SetActive(false);
    }

    public void InitializeUI()
    {
        controlScheme = PlayerPrefs.GetString("ControlScheme", "DFJK");
        musicVolume = PlayerPrefs.GetFloat("MusicVolume", 1f);
        sfxVolume = PlayerPrefs.GetFloat("SFXVolume", 1f);

        musicVolumeSlider.value = musicVolume;
        sfxVolumeSlider.value = sfxVolume;
        savedMessage.gameObject.SetActive(false);
    }

    private void PlayClickSound()
    {
        if (audioSource != null && clickSound != null)
        {
            audioSource.PlayOneShot(clickSound);
        }
    }
}
