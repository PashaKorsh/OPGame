using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SubwayPlayer : MonoBehaviour
{
    private float yCoord;
    public SubwayMain main;


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
        }
        else if (collider.gameObject.tag == "shark")
        {
            main.PlaySound("хрум");
            main.Death();
        }
        else if (collider.gameObject.tag == "trash")
        {
            main.PlaySound("wilhelm_scream");
            main.Death();
        }
        else if (collider.gameObject.name == "2")
            GameObject.Find("Main Camera").SendMessage("Win");   
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        main.PlaySound("wilhelm_scream");
        main.Death();
    }
}
