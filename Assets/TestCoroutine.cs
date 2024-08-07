using System.Collections;
using UnityEngine;

public class TestCoroutine : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite idleSprite;
    public Sprite hitSprite;
    public float duration = 1.0f;

    void Start()
    {
        StartCoroutine(SwitchSprite());
    }

    private IEnumerator SwitchSprite()
    {
        Debug.Log("Switching to hit sprite");
        spriteRenderer.sprite = hitSprite;
        yield return new WaitForSeconds(duration);
        Debug.Log("Reverting to idle sprite");
        spriteRenderer.sprite = idleSprite;
    }
}
