using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

public class SectionData
{
    public Sprite sectionSprite;
    public PhotoHolder sectionPhotoHolder;
    public PhotoInfo sectionPhotoInfo;
    bool hasData;
    public void SetData(PhotoHolder photoHolder)
    {
        sectionSprite = photoHolder.GetImage().sprite;
        sectionPhotoInfo = photoHolder.GetPhotoInfo();
        sectionPhotoHolder = photoHolder;
        hasData = true;
    }
    public bool HasData()
    {
        return hasData;
    }
    public void Clear()
    {
        sectionSprite = null;
        sectionPhotoHolder = null;
        sectionPhotoInfo = null;
        hasData = false;
    }
}
public class NewsPaperPhotoSection : MonoBehaviour, IObserver
{
    [SerializeField]
    Image sectionImage;
    [SerializeField]
    SectionData hoverData = new SectionData();
    [SerializeField]
    SectionData holdingData = new SectionData();

    [SerializeField]
    Text modiferText = null;
    [SerializeField]
    int modifer = 1;
    [SerializeField]
    PhotoListManager manager;
    [SerializeField]
    [ReadOnly]
    bool mouseIsOver = false;

    private void Awake()
    {
        PostOffice.Subscribes(this, SwitchGameStats.SWITCH_GAME_STATS_EVENT);
    }
    private void Start()
    {
        modiferText.text = "X " + modifer.ToString();
        modiferText.gameObject.SetActive(SwitchGameStats.STATS_ON);
    }
    private void OnDestroy()
    {
        PostOffice.Unsubscribes(this, SwitchGameStats.SWITCH_GAME_STATS_EVENT);
    }
    public void OnPhotoSectionEnter()
    {
        var photoHolder = NewsPaperPanel.GetInstance().GetCurrentSelection();
        if (photoHolder != null)
        {
            this.hoverData.SetData(photoHolder);
            modiferText.enabled = false;
            sectionImage.sprite = hoverData.sectionSprite;
        }
        mouseIsOver = true;

    }
    public void OnPhotoSectionExit()
    {
        this.hoverData.Clear();
        if (holdingData.HasData())
        {
            sectionImage.sprite = holdingData.sectionSprite;
        }
        else
        {
            modiferText.enabled = true;
            sectionImage.sprite = null;
        }
        mouseIsOver = false;
    }
    public void OnPhotoSectionUp()
    {
        var photoHolder = NewsPaperPanel.GetInstance().GetCurrentSelection();
        if (photoHolder && photoHolder == hoverData.sectionPhotoHolder)
        {
            if (holdingData.HasData() && holdingData.sectionPhotoHolder != photoHolder)
            {
                holdingData.sectionPhotoHolder.gameObject.SetActive(true);
                holdingData.Clear();
            }

            holdingData.SetData(photoHolder);
            photoHolder.gameObject.SetActive(false);
            sectionImage.sprite = holdingData.sectionSprite;
        }
        hoverData.Clear();
    }

    public int GetCommiePoint()
    {
        if (holdingData.sectionPhotoInfo != null)
        {
            return holdingData.sectionPhotoInfo.CommunistInfluence * this.modifer;
        }
        else
        {
            return 0;
        }
    }

    public int GetCapitalPoint()
    {
        if (holdingData.sectionPhotoInfo != null)
        {
            return holdingData.sectionPhotoInfo.CapitalistInfluence * this.modifer;
        }
        else
        {
            return 0;
        }
    }

    public void Clear()
    {
        modiferText.enabled = true;
        sectionImage.sprite = null;
        if (holdingData.HasData())
        {
            holdingData.sectionPhotoHolder.gameObject.SetActive(true);
            manager.ReturnPhotoToPile(holdingData.sectionPhotoHolder);
            holdingData.Clear();
        }
        hoverData.Clear();
    }

    public PhotoInfo GetPhotoInfo()
    {
        return holdingData.sectionPhotoInfo;
    }
    public bool HasPhoto()
    {
        return holdingData.HasData();
    }
    public void ReceiveData(DataPack pack, string eventName)
    {
        if (eventName == SwitchGameStats.SWITCH_GAME_STATS_EVENT)
        {
            modiferText.gameObject.SetActive(!modiferText.gameObject.activeSelf);
        }
    }

    public Sprite GetPhoto()
    {
        return this.holdingData.sectionSprite;
    }
}
