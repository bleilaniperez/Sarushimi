using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SushiNote : MonoBehaviour
{
    public bool canBePressed;
    public KeyCode keyToPress;
    private AudioSource audioSource;
    public AudioClip missSound;
    private bool isHit = false; 
    public Saru saruSpriteManager;
    public Transform noteSpace;
    public GameManager gameManager; 
    public HitMessageManager hitMessageManager;
    public SaruAnimationController animationController;

    
    private float perfectThreshold = 0.2f;
    private float goodThreshold = 0.3f;
    private float okThreshold = 0.4f;

    private Dictionary<string, KeyCode[]> controlSchemes = new Dictionary<string, KeyCode[]>
    {
        { "DFJK", new KeyCode[] { KeyCode.D, KeyCode.F, KeyCode.J, KeyCode.K } },
        { "Arrows", new KeyCode[] { KeyCode.LeftArrow, KeyCode.DownArrow, KeyCode.UpArrow, KeyCode.RightArrow } }
    };

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (saruSpriteManager == null)
        {
            Debug.LogError("SaruSpriteManager is not assigned.");
        }

        if (noteSpace == null)
        {
            Debug.LogError("NoteSpace is not assigned.");
        }

        if (gameManager == null)
        {
            Debug.LogError("GameManager is not assigned.");
        }

        SetKeyToPress();
    }

    // Update is called once per frame
    private void Update()
{
    if (Input.GetKeyDown(keyToPress))
    {
        if (canBePressed)
        {
            isHit = true;
            animationController.PlayHitAnimation();

            // Determine the accuracy of the hit
            string hitAccuracy = DetermineHitAccuracy();
            Vector3 hitPosition = transform.position; 
            gameManager.AddScore(hitAccuracy, hitPosition); 

            gameObject.SetActive(false);
        }
    }
}

    private void SetKeyToPress()
        {
            
        string controlScheme = PlayerPrefs.GetString("ControlScheme", "DFJK");

        if (controlSchemes.ContainsKey(controlScheme))
        {
            KeyCode[] keys = controlSchemes[controlScheme];
            switch (tag)
            {
                case "Ikura":
                    keyToPress = keys[0];
                    break;
                case "Onigiri":
                    keyToPress = keys[1];
                    break;
                case "SalmonRoll":
                    keyToPress = keys[2];
                    break;
                case "SushiNigiri":
                    keyToPress = keys[3];
                    break;
                default:
                    Debug.LogWarning("Unknown note tag: " + tag);
                    break;
            }
        }
        else
        {
            Debug.LogWarning("Control scheme not found: " + controlScheme);
        }
    }


    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "Spaces")
        {
            canBePressed = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
{
    if (other.tag == "Spaces")
    {
        if (!isHit)
        {
            animationController.PlayMissAnimation();
            Vector3 missPosition = transform.position; 

            if (AudioManager.instance != null && missSound != null)
            {
                AudioManager.instance.PlaySound(missSound);
            }

            gameManager.ResetMultiplier(); 
            gameManager.ResetComboCounter();
            GameManager.MissedNotes++; 

            if (saruSpriteManager != null)
            {
                saruSpriteManager.SwitchToMissSprite();
            }

            hitMessageManager.ShowHitFeedback("MISS", missPosition); 
        }
        canBePressed = false;
        isHit = false;
    }
}

    private string DetermineHitAccuracy()
    {
        float distance = Mathf.Abs(transform.position.y - noteSpace.position.y);

        if (distance <= perfectThreshold)
        {
            Debug.Log("Perfect hit");
            return "PERFECT";
        }
        else if (distance <= goodThreshold)
        {
            Debug.Log("Good hit");
            return "GOOD";
        }
        else 
        {
            return "OK";
        }
    }
}
