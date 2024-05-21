using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuButton : MonoBehaviour
{
    public MainMenuMain main;
    public int index;

    void OnMouseDown() => main.OnButtonClick(index);
}
