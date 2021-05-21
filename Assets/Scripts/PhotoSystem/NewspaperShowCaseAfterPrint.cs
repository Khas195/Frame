using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

public class NewspaperShowCaseAfterPrint : MonoBehaviour
{
    [SerializeField]
    bool useImages = true;
    [SerializeField]
    [ShowIf("useImages")]
    Image midPhoto;
    [SerializeField]
    [ShowIf("useImages")]
    Image leftPhoto;
    [SerializeField]
    [ShowIf("useImages")]
    Image rightPhoto;

    [SerializeField]
    [HideIf("useImages")]
    SpriteRenderer midSpritePhoto;
    [SerializeField]
    [HideIf("useImages")]
    SpriteRenderer leftSpritePhoto;
    [SerializeField]
    [HideIf("useImages")]
    SpriteRenderer rightSpritePhoto;
    public void PrintPhotos(Sprite smallPhoto, Sprite largePhoto, Sprite mediumPhoto, bool forImages = true)
    {
        if (forImages)
        {
            midPhoto.sprite = largePhoto;
            leftPhoto.sprite = mediumPhoto;
            rightPhoto.sprite = smallPhoto;
        }
        else
        {
            midSpritePhoto.sprite = largePhoto;
            leftSpritePhoto.sprite = smallPhoto;
            rightSpritePhoto.sprite = mediumPhoto;
        }
    }
    public void PrintPhotos(NewspaperData newsData, bool forImages = true)
    {
        PrintPhotos(newsData.leftArticle, newsData.mainArticle, newsData.rightArticle, forImages);
    }

    public bool HasData()
    {
        if (useImages)
        {
            return (midPhoto.sprite != null && rightPhoto.sprite != null && leftPhoto.sprite != null);
        }
        else
        {

            return (midSpritePhoto.sprite != null && leftSpritePhoto.sprite != null && rightSpritePhoto.sprite != null);
        }
    }
}
