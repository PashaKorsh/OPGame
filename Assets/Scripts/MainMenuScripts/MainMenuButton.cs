using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuButton : MonoBehaviour
{
    public int index;
    // public float pos;
    private SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        // transform.localPosition = new Vector3(0, pos * Screen.currentResolution.height / 200f, 10);
    }

    void OnMouseDown()
    {
        sr.color = new Color(0.6f,0.6f,0.6f,1f);
    }

    void OnMouseUp()
    {
        sr.color = Color.white;
        GameObject.Find("Main Camera").SendMessage("OnButtonClick", index);
    }
}
