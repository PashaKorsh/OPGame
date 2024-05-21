using System.Collections;
using UnityEngine;

public class Fader : MonoBehaviour
{
    public float fadeSpeed = 1f;

    void Fade()
    {
        var image = GetComponent<SpriteRenderer>();
        var color = image.color;
        while (color.a < 1f)
        {
            color.a += fadeSpeed * Time.deltaTime;
            image.color = color;
        }
    }

    void UnFade()
    {
        var image = GetComponent<SpriteRenderer>();
        var color = image.color;
        while (color.a > 0f)
        {
            color.a -= fadeSpeed * Time.deltaTime;
            image.color = color;
        }
    }
}
