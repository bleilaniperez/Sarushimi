using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public AudioClip song;
    // Start is called before the first frame update
    void Start()
    {
        if (AudioManager.instance != null) {
            AudioManager.instance.PlaySong(song);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
