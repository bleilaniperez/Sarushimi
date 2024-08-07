using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public GameObject optionsPanel; 
    public AudioSource audioSource;
    public AudioClip clickSound;
    public AudioClip playSound;
    public ScreenFader screenFader;

    void Start()
    {
        // Debug.Log("Fading in");
        StartCoroutine(screenFader.FadeIn());
    }

    public void StartGame()
    {
        PlayStartSound();
        StartCoroutine(StartGameSequence(0.1f));
    }

    private IEnumerator StartGameSequence(float delay)
    {
        yield return new WaitForSeconds(delay);
        yield return StartCoroutine(screenFader.FadeOut());
        SceneManager.LoadScene("SongSelect");
    }

    public void OpenOptions()
    {
        PlayClickSound();
        if (optionsPanel != null)
        {
            optionsPanel.SetActive(true);
        }
    }

    public void CloseOptions()
    {
        if (optionsPanel != null)
        {
            optionsPanel.SetActive(false);
        }
    }

    public void QuitGame()
    {
        PlayClickSound();
        Application.Quit();
    }

    private void PlayClickSound()
    {
        if (audioSource != null && clickSound != null)
        {
            audioSource.PlayOneShot(clickSound);
        }
    }

    private void PlayStartSound()
    {
        if (audioSource != null && clickSound != null)
        {
            audioSource.PlayOneShot(playSound);
        }
    }
}
