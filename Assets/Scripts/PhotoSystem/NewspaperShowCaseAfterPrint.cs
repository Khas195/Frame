using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewspaperShowCaseAfterPrint : MonoBehaviour
{
    [SerializeField]
    Image midPhoto;
    [SerializeField]
    Image leftPhoto;
    [SerializeField]
    Image rightPhoto;
    public void PrintPhotos(Sprite smallPhoto, Sprite largePhoto, Sprite mediumPhoto)
    {
        midPhoto.sprite = largePhoto;
        leftPhoto.sprite = mediumPhoto;
        rightPhoto.sprite = smallPhoto;
    }
}
