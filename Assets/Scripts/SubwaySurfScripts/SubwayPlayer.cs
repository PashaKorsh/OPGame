using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubwayPlayer : MonoBehaviour
{
    public Rigidbody2D rb2d;
    public BoxCollider2D boxCollider2D;
    public Animator animator;

    void FixedUpdate()
    {
        rb2d.AddForce(new Vector3(Input.GetAxis("Horizontal") * 50, 0f, 0f));
        animator.SetFloat("dx", rb2d.velocity[0] / 10);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
