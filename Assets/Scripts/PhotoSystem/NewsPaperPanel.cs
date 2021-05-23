using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public class NewsPaperPanel : SingletonMonobehavior<NewsPaperPanel>, IObserver
{
    [SerializeField]
    GameObject panelRoot = null;
    [SerializeField]
    PhotoListManager photoListManager = null;



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
    PaperPublishedNotifier paperPublishedNotifer;
    [SerializeField]
    PaperPublishedNotifier missingImagesNotifier;
    [SerializeField]
    PublishedPapersData publishedPapersData;
    [SerializeField]
    NewspaperShowCaseAfterPrint newspaperAfterPrint;
    [SerializeField]
    Animator printingAnim = null;
    [SerializeField]
    PublishedPapersData publishedPaperdataArchive;

    protected override void Awake()
    {
        base.Awake();
        PostOffice.Subscribes(this, SwitchGameStats.SWITCH_GAME_STATS_EVENT);
        PostOffice.Subscribes(this, GameEvent.DaySystemEvent.DAY_CHANGED_EVENT);
        publishedPapersData.Reset();
        publishedPaperdataArchive.Reset();
    }

    public bool HavePhotoInSections()
    {
        for (int i = 0; i < sections.Count; i++)
        {
            if (sections[i].HasPhoto() == true)
            {
                return true;
            }
        }
        return false;
    }

    private void OnDestroy()
    {
        PostOffice.Unsubscribes(this, SwitchGameStats.SWITCH_GAME_STATS_EVENT);
        PostOffice.Unsubscribes(this, GameEvent.DaySystemEvent.DAY_CHANGED_EVENT);
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
        this.TriggerAfterPublishCallback();
        this.printingAnim.SetBool("Printing", false);
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


    public int GetTotalCapitalistPointFromSections()
    {

        var totalPoint = 0;
        for (int i = 0; i < sections.Count; i++)
        {
            totalPoint += sections[i].GetCapitalPoint();
        }
        return totalPoint;
    }

    public int GetTotalCommiePointFromSections()
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

        totalPaperPoint.text = capitalistInfluence.Colorize(capitalistColor) + commieInfluence.Colorize(communistColor);
    }
    Action triggerPublishEventCallback = null;
    public void Publish()
    {
        var publishedPhotos = new List<PhotoInfo>();
        for (int i = 0; i < sections.Count; i++)
        {
            if (sections[i].HasPhoto())
            {
                publishedPhotos.Add(sections[i].GetPhotoInfo());
            }
            else
            {
                missingImagesNotifier.Notify();
                return;
            }
        }
        triggerPublishEventCallback = () =>
        {
            TriggerDiscardPublishedPhotoEvent(publishedPhotos);

        };

        var publicSwaySystem = PublicSwayMechanic.GetInstance();
        publicSwaySystem.AddInfluence(this.GetTotalCommiePointFromSections(), ScenarioActor.ActorFaction.Communist);
        publicSwaySystem.AddInfluence(this.GetTotalCapitalistPointFromSections(), ScenarioActor.ActorFaction.Capitalist);

        publicSwaySystem.AssignActorsAccordingToSway(fastTransition: false);
        TriggerPhotoPublishedEvent(publishedPhotos);

        this.printingAnim.SetBool("Printing", true);
    }
    public void TriggerAfterPublishCallback()
    {
        if (triggerPublishEventCallback != null)
        {
            triggerPublishEventCallback.Invoke();
            triggerPublishEventCallback = null;
        }
    }

    private void TriggerPhotoPublishedEvent(List<PhotoInfo> publishedPhoto)
    {
        paperPublishedNotifer.Notify();

        List<ScenarioActor> participatedActors = new List<ScenarioActor>();
        for (int i = 0; i < publishedPhoto.Count; i++)
        {
            participatedActors.AddRange(publishedPhoto[i].participants.ToArray());
        }

        for (int i = 0; i < participatedActors.Count; i++)
        {
            participatedActors[i].ResetInfluences();
        }


        var package = DataPool.GetInstance().RequestInstance();
        var newspaperData = new NewspaperData();
        newspaperData.mainArticle = sections[0].GetPhoto();
        newspaperData.leftArticle = sections[1].GetPhoto();
        newspaperData.rightArticle = sections[2].GetPhoto();

        newspaperAfterPrint.PrintPhotos(newspaperData);
        newspaperAfterPrint.gameObject.SetActive(true);

        publishedPapersData.paperDatas.Add(newspaperData);
        publishedPaperdataArchive.paperDatas.Add(newspaperData);
        package.SetValue(GameEvent.NewspaperEvent.PaperPublishedData.NEWSPAPER_DATA, newspaperData);


        PostOffice.SendData(package, GameEvent.NewspaperEvent.NEWSPAPER_PUBLISHED_EVENT);
        DataPool.GetInstance().ReturnInstance(package);
    }

    public void TriggerDiscardPublishedPhotoEvent(List<PhotoInfo> photoToDiscard)
    {
        var package = DataPool.GetInstance().RequestInstance();
        package.SetValue(GameEvent.PhotoEvent.DiscardPhotoEventData.PHOTO_INFOS, photoToDiscard);
        PostOffice.SendData(package, GameEvent.PhotoEvent.DISCARD_PHOTO_EVENT);
        DataPool.GetInstance().ReturnInstance(package);
    }
    public void TriggerDiscardPublishedPhotoEvent(PhotoInfo photoToDiscard)
    {
        var tempList = new List<PhotoInfo>();
        tempList.Add(photoToDiscard);
        this.TriggerDiscardPublishedPhotoEvent(tempList);
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
        else if (eventName == GameEvent.DaySystemEvent.DAY_CHANGED_EVENT)
        {
            this.publishedPapersData.Reset();
        }
    }
    public bool HasPhoto()
    {
        return photoListManager.HasPhoto();
    }
}
