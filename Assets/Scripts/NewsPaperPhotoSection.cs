using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewsPaperPhotoSection : MonoBehaviour
{
    [SerializeField]
    Image sectionImage;
    [SerializeField]
    PhotoInfo currentInfo = null;

    [SerializeField]
    bool chosen = false;
    [SerializeField]
    Text modiferText = null;
    [SerializeField]
    int modifer = 1;
    private int capitalPoint = 0;
    private int commiePoint = 0;

    private void Start()
    {
        modiferText.text = "X " + modifer.ToString();
    }
    public void OnPhotoSectionEnter()
    {
        Debug.Log("Photo Enter");
        var photoHolder = NewsPaperPanel.GetInstance().GetCurrentSelection();
        if (photoHolder != null)
        {
            this.sectionImage.sprite = photoHolder.GetImage().sprite;
            commiePoint = photoHolder.GetPhotoInfo().communistInfluence * modifer;
            capitalPoint = photoHolder.GetPhotoInfo().capitalistInfluence * modifer;
            modiferText.enabled = false;
        }

    }



    public void OnPhotoSectionExit()
    {
        if (chosen == false)
        {
            this.sectionImage.sprite = null;
            commiePoint = 0;
            capitalPoint = 0;
            modiferText.enabled = true;
        }
        else
        {
            if (currentInfo.sprite != null)
            {
                this.sectionImage.sprite = currentInfo.sprite;
                this.commiePoint = currentInfo.communistInfluence * modifer;
                this.capitalPoint = currentInfo.capitalistInfluence * modifer;
            }
            modiferText.enabled = false;
        }
    }
    public void OnPhotoSectionUp()
    {
        if (sectionImage.sprite != null)
        {
            chosen = true;
            modiferText.enabled = false;
            if (this.currentInfo.sprite != sectionImage.sprite)
            {
                this.currentInfo = NewsPaperPanel.GetInstance().GetCurrentSelection().GetPhotoInfo();
            }
        }
        else
        {
            modiferText.enabled = true;
        }
    }

    public int GetCommiePoint()
    {
        return commiePoint;
    }

    public int GetCapitalPoint()
    {
        return capitalPoint;
    }

    public void Clear()
    {
        modiferText.enabled = true;
        commiePoint = 0;
        capitalPoint = 0;
        sectionImage.sprite = null;
        chosen = false;
        this.currentInfo = new PhotoInfo();
    }
}
