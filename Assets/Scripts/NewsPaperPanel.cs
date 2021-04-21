using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class NewsPaperPanel : SingletonMonobehavior<NewsPaperPanel>, IObserver
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
    [SerializeField]
    PhotoListManager manager = null;
    [SerializeField]
    PaperPublishedNotifier notifier;


    protected override void Awake()
    {
        base.Awake();
        PostOffice.Subscribes(this, SwitchGameStats.SWITCH_GAME_STATS_EVENT);
    }
    private void OnDestroy()
    {
        PostOffice.Unsubscribes(this, SwitchGameStats.SWITCH_GAME_STATS_EVENT);
    }
    private void Start()
    {
        totalPaperPoint.gameObject.SetActive(SwitchGameStats.STATS_ON);
    }

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
        PublicSwayMechanic.GetInstance().AddInfluence(this.GetTotalCommiePointFromSections(), ScenarioActor.ActorFaction.Communist);
        PublicSwayMechanic.GetInstance().AddInfluence(this.GetTotalCapitalistPointFromSections(), ScenarioActor.ActorFaction.Capitalist);
        PublicSwayMechanic.GetInstance().AssignActorsAccordingToSway(false);

        var package = DataPool.GetInstance().RequestInstance();
        var photosToDiscard = new List<PhotoInfo>();
        for (int i = 0; i < sections.Count; i++)
        {
            photosToDiscard.Add(sections[i].GetPhotoInfo());
        }
        package.SetValue("PhotoInfos", photosToDiscard);
        PostOffice.SendData(package, PhotoListManager.DISCARD_PHOTO_EVENT);
        DataPool.GetInstance().ReturnInstance(package);
        notifier.Notify();

        InGameUIControl.GetInstance().RequestState(InGameUIState.InGameUIStateEnum.NormalState);
    }

    public void Clear()
    {
        for (int i = 0; i < sections.Count; i++)
        {
            sections[i].Clear();
        }
        UpdatePointUI(0, 0);
    }

    public void ReceiveData(DataPack pack, string eventName)
    {
        if (eventName == SwitchGameStats.SWITCH_GAME_STATS_EVENT)
        {
            this.totalPaperPoint.gameObject.SetActive(SwitchGameStats.STATS_ON);
        }
    }
}
