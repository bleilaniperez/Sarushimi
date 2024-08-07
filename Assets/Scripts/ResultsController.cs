using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class ResultsController : MonoBehaviour
{
    public ScreenFader screenFader;
    public TextMeshProUGUI missedNotesText;
    public TextMeshProUGUI perfectNotesText;
    public TextMeshProUGUI finalScoreText;
    public TextMeshProUGUI finalScoreBackdrop;
    public GameObject playAgainButton;
    public GameObject songSelectButton;
    public GameObject saru1;
    public GameObject saru2;
    public float delayBetweenResults = 1f;
    public float delayAfterPerfectNotes = 2f;
    public float buttonDelay = 1f;
    public AudioSource audioSource;
    public AudioClip resultSoundEffect;
    public AudioClip sparkleSound;
    public AudioClip click;
    public float initialPitch = 1f;

    void Start()
    {
        StartCoroutine(screenFader.FadeIn());
        saru1.SetActive(true);
        saru2.SetActive(false);
        playAgainButton.SetActive(false); 
        songSelectButton.SetActive(false);
        StartCoroutine(DisplayResults());
    }

    private IEnumerator DisplayResults()
    {

        yield return new WaitForSeconds(delayAfterPerfectNotes);
        missedNotesText.text = $"{GameManager.MissedNotes}";
        PlayResultSound(initialPitch);
        yield return new WaitForSeconds(delayBetweenResults);

        perfectNotesText.text = $"{GameManager.PerfectNotes}";
        PlayResultSound(initialPitch + 0.1f);
        yield return new WaitForSeconds(delayAfterPerfectNotes);

        finalScoreText.text = $"{GameManager.FinalScore}";
        PlayResultSound(initialPitch + 0.2f);
        finalScoreBackdrop.text = $"{GameManager.FinalScore}";

        saru1.SetActive(false);
        PlaySound(sparkleSound);
        saru2.SetActive(true);

        yield return new WaitForSeconds(buttonDelay);
        playAgainButton.SetActive(true);
        songSelectButton.SetActive(true);
    }

    private void PlayResultSound(float pitch)
    {
        audioSource.pitch = pitch;
        audioSource.PlayOneShot(resultSoundEffect);
        
    }

    private void PlaySound(AudioClip soundEffect)
    {
        audioSource.PlayOneShot(soundEffect);
    }

    public void OnPlayAgainButtonClicked()
    {
        PlaySound(click);
        StartCoroutine(StartSceneChange("Song1"));
    }

    public void OnSongSelectButtonClicked()
    {
        PlaySound(click);
        StartCoroutine(StartSceneChange("SongSelect"));
    }

    private IEnumerator StartSceneChange(String sceneName)
    {
        yield return new WaitForSeconds(0.2f);
        yield return StartCoroutine(screenFader.FadeOut());
        SceneManager.LoadScene(sceneName);
    }
}
