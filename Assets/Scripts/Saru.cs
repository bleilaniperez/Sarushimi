using System.Collections;
using UnityEngine;

public class Saru : MonoBehaviour
{
    public SpriteRenderer saruSpriteRenderer;
    public Sprite idleSprite;
    public Sprite hitSprite;
    public Sprite missSprite; 
    public float hitSpriteDuration = 0.2f;
    public float missSpriteDuration = 0.3f; 

    private Coroutine spriteCoroutine;

    public void SwitchToHitSprite()
    {
        if (spriteCoroutine != null)
        {
            StopCoroutine(spriteCoroutine);
        }
        spriteCoroutine = StartCoroutine(SwitchSpriteCoroutine(hitSprite, hitSpriteDuration));
    }

    public void SwitchToMissSprite()
    {
        if (spriteCoroutine != null)
        {
            StopCoroutine(spriteCoroutine);
        }
        spriteCoroutine = StartCoroutine(SwitchSpriteCoroutine(missSprite, missSpriteDuration));
    }

    private IEnumerator SwitchSpriteCoroutine(Sprite newSprite, float duration)
    {
        saruSpriteRenderer.sprite = newSprite;
        yield return new WaitForSeconds(duration);
        saruSpriteRenderer.sprite = idleSprite;
        spriteCoroutine = null;
    }
}
