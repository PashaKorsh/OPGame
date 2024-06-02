using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Pause : MonoBehaviour
{
    [SerializeField] private GameObject pauseWindow;
    [SerializeField] public bool _paused = false;
    [SerializeField] private bool _pressingEsc = false;
    [SerializeField] public GameObject gameManager;

    void Update()
    {
        if (Input.GetKeyDown("escape") && !_pressingEsc)
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
        pauseWindow.GetComponentInChildren<TextMeshPro>().text = IntersceneInfo.coinCount.ToString();
        gameManager.SendMessage("PauseGame");
    }

    public void OnResume()
    {
        pauseWindow.SetActive(false);
        gameManager.SendMessage("UnpauseGame");
    }
}
