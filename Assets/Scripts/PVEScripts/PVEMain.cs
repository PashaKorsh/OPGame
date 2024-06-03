using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PVEMain : MonoBehaviour
{
    public GameObject mainCamera;
    public GameObject player;
    private Animator playerAnimator;
    private AudioSource bulletSound;
    public int cameraCoeff = 10;
    public Fader fader;
    public float reload = 1f;
    private float timeReload;

    void Start()
    {
        playerAnimator = player.GetComponent<Animator>();
        bulletSound = player.GetComponent<AudioSource>();
        fader.UnFade();
    }

    void Update()
    {
        var dx = Input.GetAxis("Horizontal");
        var dy = Input.GetAxis("Vertical");
        player.transform.position += new Vector3(dx, dy, 0) * Time.deltaTime * 10f;


        var a = new Vector3(
            cameraCoeff * (Input.mousePosition[0] / Screen.width - 0.5f),
            cameraCoeff * (Input.mousePosition[1] / Screen.height - 0.5f),
            -10
        );
        mainCamera.transform.localPosition += (a - mainCamera.transform.localPosition) * Time.deltaTime * 10;
        playerAnimator.SetFloat("dx", -a[0]);
        playerAnimator.SetFloat("reverse", (dx * a[0]) >= 0 ? 1f : -1f);
        playerAnimator.SetBool("stop", Mathf.Abs(dx) < 0.1f && Mathf.Abs(dy) < 0.1f);
        
        if (Input.GetMouseButtonDown(0) && timeReload < 0.1f)
        {
            timeReload = reload;
            bulletSound.Play();
            var b = Instantiate(Resources.Load<GameObject>("Prefabs/bullet"), player.transform.position, Quaternion.identity);
            b.transform.Rotate(0,0,Mathf.Atan2(-a[0], a[1]) * Mathf.Rad2Deg + 90);
        }
        timeReload = Mathf.Max(0, timeReload - Time.deltaTime);

        if (Input.GetKeyDown("escape"))
            SceneManager.LoadScene("PowerPointScene");
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(123);
    }
}
