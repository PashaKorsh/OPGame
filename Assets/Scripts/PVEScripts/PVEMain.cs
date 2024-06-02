using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PVEMain : MonoBehaviour
{
    public GameObject mainCamera;
    public GameObject player;
    public int cameraCoeff = 10;

    void Update()
    {
        var a = new Vector3(
            cameraCoeff * (Input.mousePosition[0] / Screen.width - 0.5f),
            cameraCoeff * (Input.mousePosition[1] / Screen.height - 0.5f),
            -10
        );
        mainCamera.transform.localPosition += (a - mainCamera.transform.localPosition) * Time.deltaTime * 10;

        
        if (Input.GetMouseButtonDown(0))
        {
            var b = Instantiate(Resources.Load<GameObject>("Prefabs/bullet"), player.transform.position, Quaternion.identity);
            b.transform.Rotate(0,0,Mathf.Atan2(-a[0], a[1]) * Mathf.Rad2Deg + 90);
        }

        if (Input.GetKeyDown("escape"))
            SceneManager.LoadScene("PowerPointScene");
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(collision.gameObject);
    }
}
