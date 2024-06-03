using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DialogueHandler : MonoBehaviour
{
    public DialogueBox dialogueBox;
    public PictureHandler pictureHandler;
    public Fader fader;

    private int dialogueCounter = 0;
    private bool _pressedSpace = false;
    private List<(string Phrase, string Picture)> dialogue = new();
    private bool _isSkip = false;

    void Start()
    {
        fader.UnFade();
        ReadDialogue("dialogue" + (IntersceneInfo.dialogueNum + 1));
        dialogueBox.Write(dialogue[dialogueCounter].Phrase);
        pictureHandler.UpdatePicture(GetSprite(dialogue[dialogueCounter++].Picture));
        GameObject.Find("music").GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("Music/" + (IntersceneInfo.dialogueNum + 1) + "_dialogs");
        GameObject.Find("music").GetComponent<AudioSource>().Play();
    }

    private Sprite GetSprite(string filename)
    {
        var texture2D = Resources.Load<Texture2D>("Dialogues/" + filename);
        return Sprite.Create(texture2D, new Rect(0.0f, 0.0f, texture2D.width, texture2D.height), new Vector2(0.5f, 0.5f));
    }

    private void ReadDialogue(string filename)
    {
        dialogue = Resources.Load<TextAsset>("Dialogues/" + filename)
            .text
            .Split("\n")
            .Select(x => (x.Split("|")[0], x.Split("|")[1].Trim()))
            .ToList();
    }

    void Update()
    {
        if (Input.GetKeyDown("space") && !_pressedSpace)
        {
            if (dialogueCounter == dialogue.Count)
                EndDialogue();
            else
            {
                dialogueBox.Write(dialogue[dialogueCounter].Phrase);
                pictureHandler.UpdatePicture(GetSprite(dialogue[dialogueCounter++].Picture));
                _pressedSpace = true;
            }
        }
        else
            _pressedSpace = false;
        
        if (Input.GetKeyDown("escape"))
            EndDialogue();
    }

    void EndDialogue()
    {
        if (!_isSkip)
            fader.Fade(IntersceneInfo.games[IntersceneInfo.dialogueNum++]);
        _isSkip = true;
    }
}
