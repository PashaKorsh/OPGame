using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubwayDeath : MonoBehaviour
{
    public float time;
    private SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.transform.localScale = new Vector3(
            Camera.main.orthographicSize * 200f * Camera.main.aspect / sr.sprite.rect.width,
            Camera.main.orthographicSize * 200f * Camera.main.aspect / sr.sprite.rect.width,
            1
        );
    }

    public IEnumerator Fade()
    {
        GetComponent<AudioSource>().Play();
        var a = 0f;
        var c = sr.color;
        while (a < time)
        {
            c.a = a / time;
            a += Time.deltaTime;
            sr.color = c;
            yield return null;
        }
        a = 0f;
        while (a < time)
        {
            a += Time.deltaTime;
            yield return null;
        }
        a = time;
        while (a > 0)
        {
            c.a = a / time;
            a -= Time.deltaTime;
            sr.color = c;
            yield return null;
        }
        GameObject.Find("Main Camera").SendMessage("Restart");
    }
}
