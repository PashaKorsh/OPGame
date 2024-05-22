using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fader : MonoBehaviour
{
    public float fadeSpeed = 1f;

    void Start()
    {
        transform.localScale = new Vector3(Screen.width / 64, Screen.height / 64, 1);
    }

    public IEnumerator Fade(string sceneName = "")
    {
        var image = GetComponent<SpriteRenderer>();
        var color = image.color;
        while (color.a < 1f)
        {
            color.a += fadeSpeed * Time.deltaTime;
            image.color = color;
            yield return null;
        }
        if (sceneName != "")
            SceneManager.LoadScene(sceneName);
    }

    public IEnumerator UnFade()
    {
        var image = GetComponent<SpriteRenderer>();
        var color = image.color;
        while (color.a > 0f)
        {
            color.a -= fadeSpeed * Time.deltaTime;
            image.color = color;
            yield return null;
        }
    }
}
