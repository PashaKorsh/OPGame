using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PictureHandler : MonoBehaviour
{
    public GameObject picturePlaceholder;
    public SpriteRenderer spriteRenderer;

    public void UpdatePicture(Sprite sprite)
    {
        spriteRenderer.sprite = sprite;
        picturePlaceholder.transform.localScale = new Vector3(2 * Screen.width / sprite.rect.width, 2 * Screen.height / sprite.rect.height, 1);
    }
}
