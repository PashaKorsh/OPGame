using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubwayShark : MonoBehaviour
{
    public float speed;
    private Vector3 direction;

    void Start()
    {
        direction = new Vector3(
            Mathf.Cos((transform.eulerAngles[2] - 90) * Mathf.Deg2Rad),
            Mathf.Sin((transform.eulerAngles[2] - 90) * Mathf.Deg2Rad),
            0
        );
    }

    public void CalculateNext()
    {
        transform.position += direction * speed * Time.deltaTime;
    }
}
