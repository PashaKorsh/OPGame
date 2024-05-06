using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DialogueBox : MonoBehaviour
{
    public TextMesh textMesh;
    private List<string> dialogue = new();
    private int currentState = 0;

    public void ReadDialogue(string filename)
    {
        dialogue = System.Text.Encoding.Default.GetString(File.ReadAllBytes(".\\Assets\\Dialogues\\" + filename)).Split("|").ToList();
    }

    public bool WriteNext()
    {
        if (currentState == dialogue.Count)
            return true;
        Write(dialogue[currentState++]);
        return false;
    }

    public void Write(string message)
    {
        textMesh.text = message;
    }

    public void FixedUpdate()
    {

    }
}
