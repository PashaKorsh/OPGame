using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEditor.U2D.Sprites;

public class SubwayMain : MonoBehaviour
{
    public GameObject field;
    public GameObject picture;

    void Start()
    {
        
    }

    void FixedUpdate()
    {
        field.transform.position += new Vector3(0f, -0.1f, 0f);
        picture.transform.position += new Vector3(0f, -0.05f, 0f);
    }
}
