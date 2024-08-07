using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    private AudioSource audioSource;
    private AudioSource songAudioSource;
    private AudioSource multiplierIncreaseAudioSource;

    // AudioMixer groups
    public AudioMixerGroup musicGroup;
    public AudioMixerGroup sfxGroup;

    // Audio clips for specific sound effects
    public AudioClip hitSound;
    public AudioClip perfectHitSound;
    public AudioClip missSound;
    public AudioClip multiplierIncreaseSound;
    public AudioClip multiplierResetSound;
    public float multiplierIncreaseVolume = 0.6f;

    public event Action OnSongComplete;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            audioSource = gameObject.AddComponent<AudioSource>();
            songAudioSource = gameObject.AddComponent<AudioSource>();
            multiplierIncreaseAudioSource = gameObject.AddComponent<AudioSource>();

            songAudioSource.outputAudioMixerGroup = musicGroup;
            audioSource.outputAudioMixerGroup = sfxGroup;
            multiplierIncreaseAudioSource.outputAudioMixerGroup = sfxGroup;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        // Check if song has finished playing
        if (songAudioSource != null && !songAudioSource.isPlaying && songAudioSource.time > 0)
        {
            OnSongComplete?.Invoke();
        }
    }

    // Play a sound effect
    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }

    // Play the hit sound
    public void PlayHitSound()
    {
        PlaySound(hitSound);
    }

    // Play the perfect hit sound
    public void PlayPerfectHitSound()
    {
        PlaySound(perfectHitSound);
    }

    // Play the miss sound
    public void PlayMissSound()
    {
        PlaySound(missSound);
    }

    // Play the multiplier increase sound with its specific volume
    public void PlayMultiplierIncreaseSound()
    {
        if (multiplierIncreaseSound != null)
        {
            multiplierIncreaseAudioSource.PlayOneShot(multiplierIncreaseSound);
        }
    }

    // Play the multiplier reset sound
    public void PlayMultiplierResetSound()
    {
        PlaySound(multiplierResetSound);
    }

    // Play the background song
    public void PlaySong(AudioClip songClip)
    {
        if (songAudioSource != null && songClip != null)
        {
            songAudioSource.clip = songClip;
            songAudioSource.Play();
        }
    }

    // Stop the background song
    public void StopSong()
    {
        if (songAudioSource != null && songAudioSource.isPlaying)
        {
            songAudioSource.Stop();
        }
    }

    // Pause the background song
    public void PauseSong()
    {
        if (songAudioSource != null && songAudioSource.isPlaying)
        {
            songAudioSource.Pause();
        }
    }

    // Resume the background song
    public void ResumeSong()
    {
        if (songAudioSource != null && songAudioSource.clip != null && !songAudioSource.isPlaying)
        {
            songAudioSource.UnPause();
        }
    }

    // Set the pitch of the song
    public void SetSongPitch(float pitch)
    {
        if (songAudioSource != null)
        {
            songAudioSource.pitch = pitch;
        }
    }

    // Set the volume of the song
    public void SetSongVolume(float volume)
    {
        if (songAudioSource != null)
        {
            songAudioSource.volume = volume;
        }
    }
    
    public bool IsSongPlaying()
    {
        return songAudioSource != null && songAudioSource.isPlaying;
    }
}
