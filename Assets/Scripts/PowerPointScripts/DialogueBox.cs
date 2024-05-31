using System.Collections;
using UnityEngine;
using TMPro;

public class DialogueBox : MonoBehaviour
{
    public TextMeshPro textMesh;
    public float speed;
    private Coroutine currentCoroutine;
    private bool _writing;

    public void Write(string message)
    {
        if(_writing == true)
            StopCoroutine(currentCoroutine);
        _writing = true;
        currentCoroutine = StartCoroutine(aaabbb(message));
    }

    IEnumerator aaabbb(string message)
    {
        var a = 0f;
        while (a <= message.Length)
        {
            a += speed * Time.deltaTime;
            textMesh.text = message.Substring(0, Mathf.Min((int)a, message.Length));
            yield return null;
        }
        _writing = false;
    }
}