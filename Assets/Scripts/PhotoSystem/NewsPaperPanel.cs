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
    PaperPublishedNotifier notifier;
    [SerializeField]
    PaperPublishedNotifier missingImagesNotifier;
    [SerializeField]
    PublishedPapersData publishedPapersData;


    protected override void Awake()
    {
        base.Awake();
        PostOffice.Subscribes(this, SwitchGameStats.SWITCH_GAME_STATS_EVENT);
        PostOffice.Subscribes(this, GameEvent.DaySystemEvent.DAY_CHANGED_EVENT);
        publishedPapersData.Reset();
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


        var publicSwaySystem = PublicSwayMechanic.GetInstance();
        publicSwaySystem.AddInfluence(this.GetTotalCommiePointFromSections(), ScenarioActor.ActorFaction.Communist);
        publicSwaySystem.AddInfluence(this.GetTotalCapitalistPointFromSections(), ScenarioActor.ActorFaction.Capitalist);

        publicSwaySystem.AssignActorsAccordingToSway(fastTransition: false);



        TriggerPhotoPublishedEvent(publishedPhotos);
        TriggerDiscardPublishedPhotoEvent(publishedPhotos);

        InGameUIControl.GetInstance().RequestState(InGameUIState.InGameUIStateEnum.NormalState);
    }

    private void TriggerPhotoPublishedEvent(List<PhotoInfo> publishedPhoto)
    {
        notifier.Notify();

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
        publishedPapersData.paperDatas.Add(newspaperData);
        package.SetValue(GameEvent.NewspaperEvent.PaperPublishedData.NEWSPAPER_DATA, newspaperData);


        PostOffice.SendData(package, GameEvent.NewspaperEvent.NEWSPAPER_PUBLISHED_EVENT);
        DataPool.GetInstance().ReturnInstance(package);
    }

    private void TriggerDiscardPublishedPhotoEvent(List<PhotoInfo> photoToDiscard)
    {
        var package = DataPool.GetInstance().RequestInstance();
        package.SetValue(GameEvent.PhotoEvent.DiscardPhotoEventData.PHOTO_INFOS, photoToDiscard);
        PostOffice.SendData(package, GameEvent.PhotoEvent.DISCARD_PHOTO_EVENT);
        DataPool.GetInstance().ReturnInstance(package);
        notifier.Notify();
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
}
