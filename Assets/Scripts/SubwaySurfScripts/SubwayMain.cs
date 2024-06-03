using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SubwayMain : MonoBehaviour
{
    public GameObject field;
    public GameObject picture;
    public SubwayPlayer player;
    public SubwayDeath death;
    public Fader fader;

    private SubwayPause pause;
    private Rigidbody2D rb2d;
    private Animator animator;
    private Vector3 velocity;
    private SubwayShark[] subwaySharks;
    private AudioSource audioSource;
    public bool dead = false;


    void Start()
    {
        IntersceneInfo.coinCount = 0;
        IntersceneInfo.attempts++;
        audioSource = GetComponent<AudioSource>();
        rb2d = player.GetComponent<Rigidbody2D>();
        animator = player.GetComponent<Animator>();
        subwaySharks = GameObject.FindGameObjectsWithTag("shark").Select(x => x.GetComponent<SubwayShark>()).ToArray();

        pause = GetComponent<SubwayPause>();
        if (IntersceneInfo.subwayNeedInstruction)
        {
            fader.UnFade();
            pause._paused = true;
            pause.OnPause();
            IntersceneInfo.subwayNeedInstruction = false;
        }
        else
            fader.gameObject.GetComponent<SpriteRenderer>().color = new Color(0,0,0,0);
    }

    void PauseGame()
    {
        velocity = rb2d.velocity;
        rb2d.velocity = Vector3.zero;
    }

    void UnpauseGame()
    {
        rb2d.velocity = velocity;
    }

    void FixedUpdate()
    {
        if (!pause._paused && !dead)
            CalculateNext();
    }

    void CalculateNext()
    {
        field.transform.position += new Vector3(0f, -0.1f, 0f);
        picture.transform.position += new Vector3(0f, -0.05f, 0f);
        rb2d.AddForce(new Vector3(Input.GetAxis("Horizontal") * 50, 0f, 0f));
        animator.SetFloat("dx", rb2d.velocity[0] / 10);
        foreach (var shark in subwaySharks)
            shark.CalculateNext();
    }

    public void Death()
    {
        GameObject.Find("music").GetComponent<AudioSource>().Stop();
        pause._paused = true;
        rb2d.velocity = Vector3.zero;
        dead = true;
        StartCoroutine(death.Fade());
    }

    void Restart()
    {
        SceneManager.LoadScene("SubwaySurfGame");
    }

    void Win()
    {
        fader.Fade("PowerPointScene");
    }


    public void PlaySound(string name)
    {
        audioSource.clip = Resources.Load<AudioClip>("Music/" + name);
        audioSource.Play();
    }
}
