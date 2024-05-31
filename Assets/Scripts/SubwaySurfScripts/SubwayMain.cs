using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SubwayMain : MonoBehaviour
{
    public GameObject field;
    public GameObject picture;
    public SubwayPlayer player;
    public int score;

    private bool _paused;
    private Rigidbody2D rb2d;
    private Animator animator;
    private Vector3 velocity;
    private SubwayShark[] subwaySharks;


    void Start()
    {
        rb2d = player.GetComponent<Rigidbody2D>();
        animator = player.GetComponent<Animator>();
        subwaySharks = GameObject.FindGameObjectsWithTag("shark").Select(x => x.GetComponent<SubwayShark>()).ToArray();
    }

    void PauseGame()
    {
        velocity = rb2d.velocity;
        rb2d.velocity = Vector3.zero;
        _paused = true;
    }

    void UnpauseGame()
    {
        _paused = false;
        rb2d.velocity = velocity;
    }

    void FixedUpdate()
    {
        if (!_paused)
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

    void Restart()
    {
        SceneManager.LoadScene("SubwaySurfGame");
    }

    void Win()
    {
        SceneManager.LoadScene("PowerPointScene");
    }
}
