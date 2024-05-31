using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SokobanPlayer : MonoBehaviour
{
    public float playerSpeed;
    private bool _moving = false;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Move(Vector3 direction)
    {
        if (!_moving)
            StartCoroutine(MoveWithAnimation(direction));
    }

    IEnumerator MoveWithAnimation(Vector3 direction)
    {
        Debug.Log(direction);
        _moving = true;
        if (Mathf.Abs(direction[1]) > 0)
            animator.SetInteger("where", direction[1] > 0 ? 3 : 1);
        else
            animator.SetInteger("where", direction[0] > 0 ? 4 : 2);
        var a = 0f;
        var lastPos = transform.localPosition;
        while (a < Mathf.Abs(direction[0] + direction[1]))
        {
            a += Mathf.Abs(direction[0] + direction[1]) * Time.deltaTime * playerSpeed;
            transform.localPosition += direction * Time.deltaTime * playerSpeed;
            yield return null;
        }
        transform.localPosition = lastPos + direction;
        animator.SetInteger("where", 0);
        _moving = false;
    }
}
