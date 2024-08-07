using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections.Generic;
using System.Collections;

public class SongSelectController : MonoBehaviour
{
    public TMP_Dropdown songDropdown;
    public Button playButton;
    public Button backButton;
    public AudioSource audioSource; 
    public AudioClip playButtonClip; 
    public AudioClip backButtonClip; 
    public ScreenFader screenFader;

    private string selectedSong;

    void Start()
    {
        StartCoroutine(screenFader.FadeIn());

        songDropdown.ClearOptions();
        songDropdown.AddOptions(new List<string> { "I Must Apologize"});

        selectedSong = songDropdown.options[0].text;

        songDropdown.onValueChanged.AddListener(delegate { SongSelected(songDropdown); });

        playButton.onClick.AddListener(OnPlayButtonClicked);
        backButton.onClick.AddListener(OnBackButtonClicked);
    }

    public void SongSelected(TMP_Dropdown dropdown)
    {
        selectedSong = dropdown.options[dropdown.value].text;
        Debug.Log("Selected song: " + selectedSong);
    }

    public void OnPlayButtonClicked()
    {
        if (audioSource != null && playButtonClip != null)
        {
            audioSource.PlayOneShot(playButtonClip);
        }

        PlayerPrefs.SetString("SelectedSong", selectedSong);
        StartCoroutine(screenFader.FadeOut());
        StartCoroutine(LoadGameplaySceneAfterDelay(playButtonClip.length));
    }

    private IEnumerator LoadGameplaySceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("Song1");
    }

    public void OnBackButtonClicked()
    {
        if (audioSource != null && backButtonClip != null)
        {
            audioSource.PlayOneShot(backButtonClip);
        }
        StartCoroutine(screenFader.FadeOut());
        StartCoroutine(LoadMainMenuAfterDelay(1f));
    }

    private IEnumerator LoadMainMenuAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("MainMenu");
    }
}
