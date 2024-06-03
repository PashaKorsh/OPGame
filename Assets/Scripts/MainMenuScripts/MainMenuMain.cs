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
        picture.transform.localScale = new Vector3(
            Camera.main.orthographicSize * 200f * Camera.main.aspect / sprite.rect.width,
            Camera.main.orthographicSize * 200f / sprite.rect.height,
            1
        );
        fader.UnFade();
    }

    public void OnButtonClick(int index)
    {
        if (index == 0)
            fader.Fade("PowerPointScene");
        else if (index == 1)
            // Application.Quit();
            fader.Fade("PVEGame");
    }
}
