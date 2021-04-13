using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhotoHolder : MonoBehaviour
{
    [SerializeField]
    Image photoImage = null;

    public void SetImageSprte(Sprite newSprite)
    {
        photoImage.sprite = newSprite;
    }

    public Image GetImage()
    {
        return photoImage;
    }
}
