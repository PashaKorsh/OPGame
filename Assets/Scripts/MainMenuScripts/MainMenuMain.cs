using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuMain : MonoBehaviour
{
    public Fader fader;
    public GameObject picture;

    public void Start()
    {
        var sprite = picture.GetComponent<SpriteRenderer>().sprite;
        picture.transform.localScale = new Vector3(Screen.width / sprite.rect.width, Screen.height / sprite.rect.height, 1);
        StartCoroutine(fader.UnFade());
    }

    public void OnButtonClick(int index)
    {
        if (index == 0)
            StartCoroutine(fader.Fade("PowerPointScene"));
        else if (index == 1)
            Application.Quit();
    }
}
