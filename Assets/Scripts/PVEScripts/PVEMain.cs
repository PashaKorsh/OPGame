using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PVEMain : MonoBehaviour
{
    public float speed = 5f;
    public GameObject mainCamera;
    public GameObject field;
    public GameObject player;
    public GameObject pauseWindow;
    private Animator playerAnimator;
    private AudioSource bulletSound;
    public int cameraCoeff = 10;
    public Fader fader;
    public float reload = 1f;
    private float timeReload;
    public bool _paused = true;
    private CapsuleCollider2D col;
    private Vector2 camOffset;
    public TextMeshPro text1;
    public TextMeshPro text2;
    private float timer = 30f;
    public float inwul = 0f;
    private List<Transform> spawners;

    void Start()
    {
        spawners = GameObject.Find("spawners").GetComponentsInChildren<Transform>().ToList();
        camOffset = new Vector2(
            field.transform.localScale[0] * field.GetComponent<SpriteRenderer>().sprite.rect.width / 200f - Screen.currentResolution.width / 200f,
            field.transform.localScale[1] * field.GetComponent<SpriteRenderer>().sprite.rect.height / 200f - Screen.currentResolution.height / 200f
        );
        pauseWindow.SetActive(_paused);
        col = player.GetComponent<CapsuleCollider2D>();
        playerAnimator = GameObject.Find("sprite").GetComponent<Animator>();
        bulletSound = player.GetComponent<AudioSource>();
        fader.UnFade();
    }

    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            _paused = !_paused;
            pauseWindow.SetActive(_paused);
        }
        if (_paused) return;

        if (timer >= 0)
        {
            text1.text = timer.ToString("N2");
            timer -= Time.deltaTime;
        }
        else
            Win();
        text2.text = IntersceneInfo.pveCoin.ToString();
        inwul -= Time.deltaTime;

        var dx = Input.GetAxis("Horizontal");
        var dy = Input.GetAxis("Vertical");
        player.transform.position += new Vector3(dx, dy, 0) * Time.deltaTime * speed;

        var a = new Vector3(
            cameraCoeff * (Input.mousePosition[0] / Screen.width - 0.5f),
            cameraCoeff * (Input.mousePosition[1] / Screen.height - 0.5f),
            -10
        );
        mainCamera.transform.localPosition += (a - mainCamera.transform.localPosition) * Time.deltaTime * 10;
        mainCamera.transform.position = new Vector3(
            Mathf.Min(mainCamera.transform.position[0], camOffset[0]),
            Mathf.Min(mainCamera.transform.position[1], camOffset[1]),
            -10f
        );
        mainCamera.transform.position = new Vector3(
            Mathf.Max(mainCamera.transform.position[0], -camOffset[0]),
            Mathf.Max(mainCamera.transform.position[1], -camOffset[1]),
            -10f
        );
        var c = Camera.main.ScreenToWorldPoint(Input.mousePosition) - player.transform.position;
        playerAnimator.SetFloat("dx", -c[0]);
        playerAnimator.SetFloat("reverse", (dx * c[0]) >= 0 ? 1f : -1f);
        playerAnimator.SetBool("stop", Mathf.Abs(dx) < 0.1f && Mathf.Abs(dy) < 0.1f);
        if (Input.GetMouseButtonDown(0) && timeReload < 0.1f)
        {
            timeReload = reload;
            bulletSound.Play();
            var bullet = Instantiate(
                Resources.Load<GameObject>("Prefabs/bullet"),
                player.transform.position,
                Quaternion.identity
            );
            Physics2D.IgnoreCollision(bullet.GetComponent<BoxCollider2D>(), col);
            bullet.transform.Rotate(0,0,Mathf.Atan2(-c[0], c[1]) * Mathf.Rad2Deg + 90);
        }
        timeReload = Mathf.Max(0, timeReload - Time.deltaTime);
    }

    void Win()
    {
        fader.Fade("PowerPointScene");
    }

    public void Spawn()
    {
        while (true)
        {
            var a = spawners[Mathf.FloorToInt(Random.value * spawners.Count)];
            if ((a.position - Camera.main.transform.position).sqrMagnitude > 225)
            {
                if (Random.value < 0.5f)
                    Instantiate(Resources.Load<GameObject>("Prefabs/bobr1"), a.position, Quaternion.identity);
                else
                    Instantiate(Resources.Load<GameObject>("Prefabs/bobr2"), a.position, Quaternion.identity);
                break;
            }
        }
    }
}
