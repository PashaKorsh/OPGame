using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PVEBobr : MonoBehaviour
{
    public float speed;
    private Transform player;
    private Rigidbody2D rb2d;
    private Animator animator;
    private PVEMain main;

    void Start()
    {
        main = GameObject.Find("mainObject").GetComponent<PVEMain>();
        player = GameObject.Find("player").transform;
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (main._paused) return;
        var a = player.position - transform.position;
        a[2] = 0f;
        a = a.normalized;
        animator.SetFloat("dx", -a[0]);
        animator.SetFloat("dy", a[1]);
        transform.position += a * speed * Time.deltaTime;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.name.Contains("bullet"))
        {
            Destroy(collision.collider.gameObject);
            Destroy(gameObject);
        }
    }
}
