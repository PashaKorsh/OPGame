using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishButton : MonoBehaviour
{
    private SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void OnMouseDown()
    {
        sr.color = new Color(0.6f,0.6f,0.6f,1f);
    }

    void OnMouseUp()
    {
        sr.color = Color.white;
        IntersceneInfo.dialogueNum = 0;
        IntersceneInfo.coinCount = 0;
        IntersceneInfo.attempts = 0;
        IntersceneInfo.subwayNeedInstruction = true;
        IntersceneInfo.sokoNum = 0;
        IntersceneInfo.sokoCoin = 0;
        IntersceneInfo.sokoNeedInstruction = true;
        IntersceneInfo.pveCoin = 0;
        GameObject.Find("fader").GetComponent<Fader>().Fade("MainMenuScene");
    }
}
