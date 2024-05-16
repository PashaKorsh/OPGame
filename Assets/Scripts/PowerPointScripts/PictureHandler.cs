using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PictureHandler : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    public void UpdatePicture(Sprite sprite)
    {
        spriteRenderer.sprite = sprite;
    }
}
