using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SubwayPause : MonoBehaviour
{
    [SerializeField] private GameObject pauseWindow;
    [SerializeField] public bool _paused = false;
    [SerializeField] private bool _pressingEsc = false;
    [SerializeField] public GameObject gameManager;
    private SubwayMain main;

    void Start()
    {
        main = GetComponent<SubwayMain>();
    }

    void Update()
    {
        if (Input.GetKeyDown("escape") && !_pressingEsc && !main.dead)
        {
            _pressingEsc = true;
            _paused = !_paused;
            if (_paused)
                OnPause();
            else
                OnResume();
        } else
            _pressingEsc = false;
    }

    public void OnPause()
    {
        pauseWindow.SetActive(true);
        GameObject.Find("text1").GetComponent<TextMeshPro>().text = IntersceneInfo.coinCount.ToString();
        GameObject.Find("text2").GetComponent<TextMeshPro>().text = IntersceneInfo.attempts.ToString();
        gameManager.SendMessage("PauseGame");
    }

    public void OnResume()
    {
        pauseWindow.SetActive(false);
        gameManager.SendMessage("UnpauseGame");
    }
}
