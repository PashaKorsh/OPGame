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
    public Fader fader;

    private int dialogueCounter = 0;
    private bool _pressedSpace = false;
    private List<(string Phrase, string Picture)> dialogue = new();

    void Start()
    {
        StartCoroutine(fader.UnFade());
        ReadDialogue("text");
        dialogueBox.Write(dialogue[dialogueCounter].Phrase);
        pictureHandler.UpdatePicture(GetSprite(dialogue[dialogueCounter++].Picture));
    }

    private Sprite GetSprite(string filename)
    {
        var texture2D = Resources.Load<Texture2D>(filename);
        return Sprite.Create(texture2D, new Rect(0.0f, 0.0f, texture2D.width, texture2D.height), new Vector2(0.5f, 0.5f));
    }

    private void ReadDialogue(string filename)
    {
        dialogue = Resources.Load<TextAsset>(filename)
            .text
            .Split("|")
            .Select(x => (x.Split("\\")[0], x.Split("\\")[1]))
            .ToList();
        
    }

    void Update()
    {
        if (Input.GetKeyDown("space") && !_pressedSpace)
        {
            if (dialogueCounter == dialogue.Count)
            {
                StartCoroutine(fader.Fade("SubwaySurfGame"));
                return;
            }
            dialogueBox.Write(dialogue[dialogueCounter].Phrase);
            pictureHandler.UpdatePicture(GetSprite(dialogue[dialogueCounter++].Picture));
            _pressedSpace = true;
        }
        else
            _pressedSpace = false;
    }
}
