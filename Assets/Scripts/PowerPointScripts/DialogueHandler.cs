using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using System.IO;

public class DialogueHandler : MonoBehaviour
{
    public DialogueBox dialogueBox;
    public PictureHandler pictureHandler;

    private int dialogueCounter = 0;
    private bool _pressedSpace = false;
    private List<(string Phrase, Sprite Picture)> dialogue = new();

    void Start()
    {
        ReadDialogue("text.txt");
    }

    private void ReadDialogue(string filename)
    {
        dialogue = System.Text.Encoding.Default.GetString(File.ReadAllBytes(".\\Assets\\Dialogues\\" + filename))
            .Split("|").Select(x => {
                Debug.Log(x.Split("\\")[1]);
                return (x.Split("\\")[0], Resources.Load<Sprite>(x.Split("\\")[1]));
            }).ToList();
    }

    void Update()
    {
        if (Input.GetKeyDown("space") && !_pressedSpace)
        {
            if (dialogueCounter == dialogue.Count)
            {
                SceneManager.LoadScene("TestScene");
                return;
            }
            dialogueBox.Write(dialogue[dialogueCounter].Phrase);
            pictureHandler.UpdatePicture(dialogue[dialogueCounter++].Picture);
            _pressedSpace = true;
        }
        else
            _pressedSpace = false;
    }
}
