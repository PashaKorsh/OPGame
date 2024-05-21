using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    public Player player;
    public BoxCollider2D floor;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.D))
            player.rb2d.AddForce(new Vector3(1f,0f,0f));
        if (Input.GetKey(KeyCode.A))
            player.rb2d.AddForce(new Vector3(-1f,0f,0f));
        if (Input.GetKey(KeyCode.W) && floor.IsTouching(player.boxCollider2D))
            player.rb2d.AddForce(new Vector3(0f,10f,0f));
    }
}
