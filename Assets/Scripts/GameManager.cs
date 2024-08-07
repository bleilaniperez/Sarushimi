using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static int MissedNotes { get; set; }
    public static int PerfectNotes { get; private set; }
    public static int FinalScore { get; private set; }

    public HitMessageManager hitMessageManager;
    public AudioClip gameSong; 
    public AudioClip levelCompleteJingle;
    public TextMeshProUGUI startMessage;
    public TextMeshProUGUI endMessage;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI multiplierText;
    public TextMeshProUGUI comboCounterText;
    public ScreenFader screenFader;
    public float songPitch = 0.92f;
    public float songVolume = 0.4f;

    private int score = 0;
    private int multiplier = 1;
    private int consecutiveHits = 0;
    private int comboHits = 0;
    private int hitsForNextMultiplier = 10;
    private bool songHasStarted = false;
    private bool levelCompleted = false;
    private bool levelCompletionHandled = false; //prevent multiple calls

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (songHasStarted && !AudioManager.instance.IsSongPlaying() && !levelCompletionHandled)
        {
            StartCoroutine(HandleLevelComplete());
            levelCompletionHandled = true; 
        }
    }

    void Start()
    {
        StartCoroutine(screenFader.FadeIn());

        if (startMessage != null)
        {
            startMessage.gameObject.SetActive(true);
        }

        if (endMessage != null)
        {
            endMessage.gameObject.SetActive(false);
        }

        if (comboCounterText != null)
        {
            comboCounterText.gameObject.SetActive(false); 
        }

        UpdateScoreText();
        UpdateMultiplierText();
    }

    public void StartGame()
    {
        startMessage.gameObject.SetActive(false);

        if (AudioManager.instance != null)
        {
            AudioManager.instance.SetSongPitch(songPitch);
            AudioManager.instance.SetSongVolume(songVolume);
            AudioManager.instance.PlaySong(gameSong);
            songHasStarted = true;
        }
    }

    private void HandleSongComplete()
    {
        if (endMessage != null)
        {
            endMessage.gameObject.SetActive(true);
        }
        Debug.Log("Song Complete!");
    }

    private IEnumerator HandleLevelComplete()
    {
        FinalScore = score;
        if (levelCompleteJingle != null)
        {
            AudioSource.PlayClipAtPoint(levelCompleteJingle, Vector3.zero);
            yield return new WaitForSeconds(levelCompleteJingle.length);
        }

        yield return StartCoroutine(screenFader.FadeOut());
        SceneManager.LoadScene("Results");
    }

    private void OnDestroy()
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.OnSongComplete -= HandleSongComplete;
        }
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit Game");
    }

    public void AddScore(string hitRating, Vector3 hitPosition)
    {
        int points = 0;

        switch (hitRating)
        {
            case "PERFECT":
                PerfectNotes++;
                AudioManager.instance.PlayPerfectHitSound();
                hitMessageManager.ShowHitFeedback("PERFECT", hitPosition);
                points = 100;
                break;
            case "GOOD":
                AudioManager.instance.PlayHitSound();
                hitMessageManager.ShowHitFeedback("GOOD", hitPosition);
                points = 50;
                break;
            case "OK":
                AudioManager.instance.PlayHitSound();
                hitMessageManager.ShowHitFeedback("OK", hitPosition);
                points = 25;
                break;
            default:
                return; 
        }

        score += points * multiplier;
        consecutiveHits++;
        comboHits++;

       
        if (comboHits >= 2)
        {
            UpdateComboCounter(comboHits);
        }

        if (consecutiveHits >= hitsForNextMultiplier && multiplier < 5)
        {
            multiplier++;
            consecutiveHits = 0; 
            AudioManager.instance.PlayMultiplierIncreaseSound();
        }

        UpdateScoreText();
        UpdateMultiplierText();
    }

    public void ResetMultiplier()
    {
        if (multiplier > 1)
        {
            AudioManager.instance.PlayMultiplierResetSound();
        }

        multiplier = 1;
        consecutiveHits = 0; 
        UpdateMultiplierText();
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score.ToString();
        }
    }

    private void UpdateMultiplierText()
    {
        if (multiplierText != null)
        {
            multiplierText.text = "x" + multiplier.ToString();
        }
    }

    private void UpdateComboCounter(int hits)
    {
        if (comboCounterText != null)
        {
            comboCounterText.text = hits.ToString();
            comboCounterText.gameObject.SetActive(true);
        }
    }

    public void ResetComboCounter()
    {
        comboHits = 0;
        if (comboCounterText != null)
        {
            comboCounterText.gameObject.SetActive(false);
        }
    }
}
