using UnityEngine;

public class DialogueBox : MonoBehaviour
{
    public TextMesh textMesh;

    public void Write(string message)
    {
        textMesh.text = message;
    }
}