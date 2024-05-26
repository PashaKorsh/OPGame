using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubwayMain : MonoBehaviour
{
    public GameObject field;
    public GameObject picture;
    public Pause pause;

    void FixedUpdate()
    {
        if (!pause._paused)
            CalculateNext();
    }

    void CalculateNext()
    {
        field.transform.position += new Vector3(0f, -0.1f, 0f);
        picture.transform.position += new Vector3(0f, -0.05f, 0f);
    }
}
