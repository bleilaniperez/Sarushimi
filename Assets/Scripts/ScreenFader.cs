using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFader : MonoBehaviour
{
    private Image whiteOverlay;
    public float fadeOutDuration = 1f;
    public float fadeInDuration = 1f;

    void Start()
    {
        whiteOverlay = GetComponent<Image>();
        if (whiteOverlay == null)
        {
            Debug.LogError("Image component not found on GameObject.");
        }
        else
        {
            Color tempColor = whiteOverlay.color;
            tempColor.a = 1f; 
            whiteOverlay.color = tempColor;
        }
    }


    public IEnumerator FadeIn()
    {
        if (whiteOverlay == null)
        {
            whiteOverlay = GetComponent<Image>();
        }

        float elapsedTime = 0f;

    if (whiteOverlay != null)
    {
        Color tempColor = whiteOverlay.color;
        tempColor.a = 1f;
        whiteOverlay.color = tempColor;

        Debug.Log("Starting FadeIn...");
        while (elapsedTime < fadeInDuration)
        {
            elapsedTime += Time.deltaTime;
            tempColor.a = Mathf.Clamp01(1f - (elapsedTime / fadeInDuration)); 
            whiteOverlay.color = tempColor; 
            Debug.Log($"FadeIn progress: {tempColor.a}");
            yield return null; 
        }

        tempColor.a = 0f;
        whiteOverlay.color = tempColor;
        Debug.Log("FadeIn completed.");
    }
    }



    public IEnumerator FadeOut()
    {
        float elapsedTime = 0f;

        Color tempColor = whiteOverlay.color;
        tempColor.a = 0f;
        whiteOverlay.color = tempColor;

        while (elapsedTime < fadeOutDuration)
        {
            elapsedTime += Time.deltaTime;
            tempColor.a = Mathf.Clamp01(elapsedTime / fadeOutDuration);
            whiteOverlay.color = tempColor;
            yield return null;
        }

        tempColor.a = 1f;
        whiteOverlay.color = tempColor;
        
    }

}
