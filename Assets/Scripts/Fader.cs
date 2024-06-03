using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fader : MonoBehaviour
{
    public float fadeSpeed = 1f;
    public bool fading;

    void Start()
    {
        fading = false;
        transform.localScale = new Vector3(
            Camera.main.orthographicSize * 200f * Camera.main.aspect / 64,
            Camera.main.orthographicSize * 200f / 64,
            1
        );
    }

    public void Fade(string sceneName = "")
    {
        if (!fading)
            StartCoroutine(FadeCor(sceneName));
    }

    public void UnFade()
    {
        if(!fading)
            StartCoroutine(UnFadeCor());
    }

    private IEnumerator FadeCor(string sceneName)
    {
        fading = true;
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
        fading = false;
    }

    private IEnumerator UnFadeCor()
    {
        fading = true;
        var image = GetComponent<SpriteRenderer>();
        var color = image.color;
        while (color.a > 0f)
        {
            color.a -= fadeSpeed * Time.deltaTime;
            image.color = color;
            yield return null;
        }
        fading = false;
    }
}
