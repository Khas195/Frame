using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewsPaperPhotoSection : MonoBehaviour
{
    [SerializeField]
    Image sectionImage;
    [SerializeField]
    bool chosen = false;
    public void OnPhotoSectionEnter()
    {
        Debug.Log("Photo Enter");
        if (NewsPaperPanel.GetInstance().GetCurrentSelectionImage())
        {

            this.sectionImage.sprite = NewsPaperPanel.GetInstance().GetCurrentSelectionImage().sprite;
        }

    }
    public void OnPhotoSectionExit()
    {
        if (chosen == false)
        {
            this.sectionImage.sprite = null;
        }
    }
    public void OnPhotoSectionUp()
    {
        if (sectionImage.sprite != null)
        {
            chosen = true;
        }
    }
}
