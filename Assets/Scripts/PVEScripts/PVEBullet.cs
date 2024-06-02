using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PVEBullet : MonoBehaviour
{
    [SerializeField] private float speed;
    private Vector3 direction; 

    void Start()
    {
        var z = transform.eulerAngles[2];
        direction = new Vector3(Mathf.Cos(z * Mathf.Deg2Rad), Mathf.Sin(z * Mathf.Deg2Rad), 0);
    }

    void Update()
    {
        transform.position += direction * Time.deltaTime * speed;
    }
}
