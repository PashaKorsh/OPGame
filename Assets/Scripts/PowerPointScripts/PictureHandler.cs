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
        picturePlaceholder.transform.localScale = new Vector3(
            Camera.main.orthographicSize * 200f * Camera.main.aspect / sprite.rect.width,
            Camera.main.orthographicSize * 200f / sprite.rect.height,
            1
        );
    }
}
