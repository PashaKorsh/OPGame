using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuMain : MonoBehaviour
{
    public Fader fader;

    public void OnButtonClick(int index)
    {
        if (index == 0)
            SceneManager.LoadScene("PowerPointScene");
        else if (index == 1)
            Application.Quit();
    }
}
