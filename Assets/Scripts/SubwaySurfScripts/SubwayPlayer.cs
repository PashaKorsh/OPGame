using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SubwayPlayer : MonoBehaviour
{
    private float yCoord;


    void Start()
    {
        yCoord = -0.5f * Screen.height / 200f;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "coin")
        {
            IntersceneInfo.coinCount++;
            Destroy(collider.gameObject);
            return;
        } else if (collider.gameObject.tag == "shark")
            GameObject.Find("Main Camera").SendMessage("Restart");
        else if (collider.gameObject.tag == "trash")
            GameObject.Find("Main Camera").SendMessage("Restart");
        // SceneManager.LoadScene("SubwaySurfGame");
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject.Find("Main Camera").SendMessage("Win");
    }
}
