using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonStart : MonoBehaviour
{
    void OnMouseDown()
    {
        SceneManager.LoadScene("PowerPointScene");
    }
}
