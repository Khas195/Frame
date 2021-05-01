using System;
using System.Collections;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

public class DaySystem : SingletonMonobehavior<DaySystem>, IObserver
{
    [SerializeField]
    [Expandable]
    DaySystemData dayData;
    [SerializeField]
    PublishedPapersData publishedPaperData;
    [SerializeField]
    GameObject nextDayButton = null;
    [SerializeField]
    Text nextDayText = null;
    [SerializeField]
    FadeTransition textFadeTrans = null;
    [SerializeField]
    GameInstance reviewInstance;


    protected override void Awake()
    {
        PostOffice.Subscribes(this, GameEvent.NewspaperEvent.NEWSPAPER_PUBLISHED_EVENT);
        dayData.ResetDay();
    }
    private void OnDestroy()
    {
        PostOffice.Unsubscribes(this, GameEvent.NewspaperEvent.NEWSPAPER_PUBLISHED_EVENT);
    }

    public int GetCurrentDay()
    {
        return dayData.currentDay;
    }

    private void Update()
    {

        if (CanProceedToNextDay())
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                TriggerReviewPanel();
                textFadeTrans.FadeOut();
            }
        }
    }

    private void TriggerReviewPanel()
    {
        InGameUIControl.GetInstance().RequestState(InGameUIState.InGameUIStateEnum.PublishedPaperPanel);
    }

    public void NextDay()
    {
        if (IsPossibleToProceedToNextDay())
        {
            SetCurrentDay(dayData.currentDay + 1);
        }
        InGameUIControl.GetInstance().RequestState(InGameUIState.InGameUIStateEnum.NormalState);
    }

    private bool IsPossibleToProceedToNextDay()
    {
        return dayData.currentDay < dayData.amountOfPaperNeededPerDay.Count;
    }

    public void SetCurrentDay(int newDay)
    {
        dayData.currentDay = newDay;
        TriggerOnDayChangedEvent();
    }

    private void TriggerOnDayChangedEvent()
    {
        var data = DataPool.GetInstance().RequestInstance();
        data.SetValue(GameEvent.DaySystemEvent.OnDayChangedEventData.CURRENT_DAY, dayData.currentDay);
        PostOffice.SendData(data, GameEvent.DaySystemEvent.DAY_CHANGED_EVENT);
        DataPool.GetInstance().ReturnInstance(data);
    }

    public void ReceiveData(DataPack pack, string eventName)
    {
        if (eventName == GameEvent.NewspaperEvent.NEWSPAPER_PUBLISHED_EVENT)
        {
            if (CanProceedToNextDay())
            {
                textFadeTrans.FadeIn();
            }
        }
    }

    private bool CanProceedToNextDay()
    {
        return IsPossibleToProceedToNextDay() && publishedPaperData.paperDatas.Count >= dayData.amountOfPaperNeededPerDay[dayData.currentDay];
    }
}