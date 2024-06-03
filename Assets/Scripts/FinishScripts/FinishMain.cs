using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FinishMain : MonoBehaviour
{
    public GameObject picture;
    public Fader fader;
    public float time;
    private float timer = 0f;
    private Animator animator;
    private bool isEnd = false;

    void Start()
    {
        GameObject.Find("text1").GetComponent<TextMeshPro>().text = IntersceneInfo.coinCount.ToString();
        GameObject.Find("text2").GetComponent<TextMeshPro>().text = IntersceneInfo.sokoCoin.ToString();
        GameObject.Find("text3").GetComponent<TextMeshPro>().text = IntersceneInfo.pveCoin.ToString();


        GameObject.Find("text4").GetComponent<TextMeshPro>().text = (IntersceneInfo.coinCount + IntersceneInfo.sokoCoin + IntersceneInfo.pveCoin).ToString();


        time += Random.value * 2;
        animator = GetComponentInChildren<Animator>();
        Debug.Log(animator);
        fader.UnFade();
        var sr = picture.GetComponent<SpriteRenderer>();
        picture.transform.localScale = new Vector3(
            Camera.main.orthographicSize * 200f * Camera.main.aspect / sr.sprite.rect.width,
            Camera.main.orthographicSize * 200f / sr.sprite.rect.height,
            1
        );
    }

    void Update()
    {
        if (!isEnd)
        {
            timer += Time.deltaTime;
            if (timer >= time)
            {
                isEnd = true;
                animator.enabled = false;
                GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("Music/moneyjackpot");
                GetComponent<AudioSource>().Play();
            }
        }
    }
}
