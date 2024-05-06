using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogueHandler : MonoBehaviour
{
    public DialogueBox dialogueBox;
    private bool _pressedSpace = false;

    void Start()
    {
        dialogueBox.ReadDialogue("text.txt");
    }

    void Update()
    {
        var ifAll = false;
        if (Input.GetKeyDown("space") && !_pressedSpace)
        {
            ifAll = dialogueBox.WriteNext();
            _pressedSpace = true;
        }
        else
            _pressedSpace = false;
        
        if (ifAll)
            SceneManager.LoadScene("TestScene");
            // dialogueBox.Write("А ВСЁ!! КОНЧИЛОСЯ!!!");
    }
}
