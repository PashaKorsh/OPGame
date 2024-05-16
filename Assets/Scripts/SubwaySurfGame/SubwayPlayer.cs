using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubwayPlayer : MonoBehaviour
{
    public Rigidbody2D rb2d;
    public BoxCollider2D boxCollider2D;
    // public Transform transform;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // GetComponent<Animation>().Play("New Animation");
        // transform.position = new Vector3(0f,0f,0f);
        // GetComponent<Animator>().Play("Base Layer.Entry");
    }

    void FixedUpdate()
    {
        // rb2d.AddForce(new Vector3(1f,1f,0f));
        
    }
}
