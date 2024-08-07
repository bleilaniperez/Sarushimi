using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SpaceController : MonoBehaviour
{

    public KeyCode keyToPress;
    public Color normalColor;
    public Color pressedColor = Color.gray;
    public Vector3 normalScale;
    public Vector3 pressedScale;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        normalScale = transform.localScale;
        pressedScale = normalScale * 1.1f;

        spriteRenderer = GetComponent<SpriteRenderer>();
        normalColor = spriteRenderer.color;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(keyToPress))
        {
            ScaleUp();
        }

        if (Input.GetKeyUp(keyToPress)) {
            ScaleDown();
        }
    }

    void ScaleUp()
    {
        transform.localScale = pressedScale;
        spriteRenderer.color = pressedColor;
        
    }

    void ScaleDown()
    {
        transform.localScale = normalScale;
        spriteRenderer.color = normalColor;
    }
}
