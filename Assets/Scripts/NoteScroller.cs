using UnityEngine;

public class NoteScroller : MonoBehaviour
{
    public float tempo;
    private bool hasStarted;

    void Start()
    {
        tempo = tempo / 60f; // Convert BPM to speed
    }

    void Update()
    {
        if (!hasStarted)
        {
            if (Input.anyKeyDown)
            {
                hasStarted = true;
                GameManager.instance.StartGame(); // Notify GameManager that the game has started
            }
        }

        if (hasStarted)
        {
            // Move all notes downwards at the specified tempo
            transform.position -= new Vector3(0f, tempo * Time.deltaTime, 0f);
        }
    }

    public void StopScrolling()
    {
        hasStarted = false;
    }
}
