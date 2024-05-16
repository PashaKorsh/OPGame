using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.U2D.Sprites;

public class SubwayMain : MonoBehaviour
{
    public SubwayPlayer player;
    public BoxCollider2D floor;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        player.rb2d.AddForce(new Vector3(Input.GetAxis("Horizontal"),0f,0f));
        player.rb2d.AddForce(new Vector3(0f,Input.GetAxis("Vertical"),0f));
    }
}
