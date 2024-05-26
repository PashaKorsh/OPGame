using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    [SerializeField] private GameObject pauseWindow;
    [SerializeField] public bool _paused = false;
    [SerializeField] private bool _pressingEsc = false;

    void Update()
    {
        if (!Input.GetKeyDown("escape") && _pressingEsc)
            _pressingEsc = false;
        if (Input.GetKeyDown("escape") && !_pressingEsc)
        {
            _pressingEsc = true;
            _paused = !_paused;
            pauseWindow.SetActive(_paused);
        }
    }
}
