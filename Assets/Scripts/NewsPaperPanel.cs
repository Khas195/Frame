using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class NewsPaperPanel : SingletonMonobehavior<NewsPaperPanel>
{
    [SerializeField]
    GameObject panelRoot = null;
    [SerializeField]
    Image headline;
    [SerializeField]
    Image subHeadline1;



    [SerializeField]
    Image subHeadline2;
    [SerializeField]
    PhotoHolder currentDrag = null;
    [SerializeField]
    public UnityEvent OnPhotoDrop = new UnityEvent();

    public void SwitchPanelOn()
    {
        panelRoot.SetActive(true);
    }
    public void SwitchPanelOff()
    {
        panelRoot.SetActive(false);
    }

    public void SetCurrentSelection(PhotoHolder photoHolder)
    {
        this.currentDrag = photoHolder;
    }

    public Image GetCurrentSelectionImage()
    {
        if (currentDrag)
        {
            return this.currentDrag.GetImage();
        }
        else
        {
            return null;
        }
    }
}
