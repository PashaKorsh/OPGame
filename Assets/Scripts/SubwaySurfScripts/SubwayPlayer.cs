using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SubwayPlayer : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private BoxCollider2D boxCollider2D;
    private Animator animator;
    private int score;

    private Vector3 velocity;
    private bool _paused = false;

    void PauseRB2D()
    {
        velocity = rb2d.velocity;
        rb2d.velocity = Vector3.zero;
        _paused = true;
    }

    void ResumeRB2D()
    {
        rb2d.velocity = velocity;
        _paused = false;
    }

    void Start()
    {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        boxCollider2D = gameObject.GetComponent<BoxCollider2D>();
        animator = gameObject.GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if (!_paused)
            CalculateNext();
    }

    void CalculateNext()
    {
        rb2d.AddForce(new Vector3(Input.GetAxis("Horizontal") * 50, 0f, 0f));
        animator.SetFloat("dx", rb2d.velocity[0] / 10);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider is CircleCollider2D)
        {
            SceneManager.LoadScene("PVEGame");
            score++;
            Destroy(collision.gameObject);
            return;
        }
        Destroy(gameObject);
    }
}
