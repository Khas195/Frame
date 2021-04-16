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
    List<NewsPaperPhotoSection> sections = new List<NewsPaperPhotoSection>();

    [SerializeField]
    PhotoHolder currentDrag = null;
    [SerializeField]
    public UnityEvent OnPhotoDrop = new UnityEvent();
    [SerializeField]
    Text totalPaperPoint;


    public void SwitchPanelOn()
    {
        panelRoot.SetActive(true);
    }
    public void SwitchPanelOff()
    {
        panelRoot.SetActive(false);
        Clear();

    }

    public void SetCurrentSelection(PhotoHolder photoHolder)
    {
        this.currentDrag = photoHolder;
    }

    public PhotoHolder GetCurrentSelection()
    {
        return this.currentDrag;
    }
    private void Update()
    {
        if (panelRoot.activeSelf)
        {
            var totalCommiePoint = GetTotalCommiePointFromSections();
            var totalCapitalistPoint = GetTotalCapitalistPointFromSections();
            UpdatePointUI(totalCommiePoint, totalCapitalistPoint);
        }
    }

    private int GetTotalCapitalistPointFromSections()
    {

        var totalPoint = 0;
        for (int i = 0; i < sections.Count; i++)
        {
            totalPoint += sections[i].GetCapitalPoint();
        }
        return totalPoint;
    }

    private int GetTotalCommiePointFromSections()
    {
        var totalPoint = 0;
        for (int i = 0; i < sections.Count; i++)
        {
            totalPoint += sections[i].GetCommiePoint();
        }
        return totalPoint;
    }
    private void UpdatePointUI(int commiePoint, int capitalPoint)
    {
        Color communistColor = PublicSwayMechanic.GetInstance().GetColorToFaction(ScenarioActor.ActorFaction.Communist);
        Color capitalistColor = PublicSwayMechanic.GetInstance().GetColorToFaction(ScenarioActor.ActorFaction.Capitalist);

        string commieInfluence = PhotoHolder.ConvertInfluenceToString(commiePoint);
        string capitalistInfluence = PhotoHolder.ConvertInfluenceToString(capitalPoint);

        totalPaperPoint.text = commieInfluence.Colorize(communistColor) + " " + capitalistInfluence.Colorize(capitalistColor);
    }
    public void Publish()
    {

    }
    public void Clear()
    {
        for (int i = 0; i < sections.Count; i++)
        {
            sections[i].Clear();
        }
        UpdatePointUI(0, 0);
    }

}
