using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PVEPlayer : MonoBehaviour
{
    void FixedUpdate()
    {
        var dx = Input.GetAxis("Horizontal");
        var dy = Input.GetAxis("Vertical");

        transform.position += new Vector3(dx, dy, 0) * 0.2f;// / Mathf.Sqrt(dx * dx + dy * dy);
    }
}
