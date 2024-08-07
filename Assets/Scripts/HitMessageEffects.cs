using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitMessageEffects : MonoBehaviour
{
    public float floatSpeed = 1f; // Speed at which the sprite floats upwards
    public float fadeDuration = 1f; // Duration of the fade-out effect

    private SpriteRenderer spriteRenderer; // For controlling sprite color
    private Color originalColor; // Store the original color of the sprite

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            originalColor = spriteRenderer.color; // Store the original color with full alpha
        }

        // Start the floating and fading effect
        StartCoroutine(FloatAndFade());
    }

    IEnumerator FloatAndFade()
    {
        float elapsedTime = 0f;
        Vector3 originalPosition = transform.position;

        while (elapsedTime < fadeDuration)
        {
            // Float upwards
            transform.position = originalPosition + new Vector3(0, floatSpeed * elapsedTime, 0);

            // Fade out
            if (spriteRenderer != null)
            {
                float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
                spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure fully faded out and destroy the object
        if (spriteRenderer != null)
        {
            spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);
        }
        Destroy(gameObject);
    }
}
