using System;
using System.Collections;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DaySystem : SingletonMonobehavior<DaySystem>, IObserver
{
    [SerializeField]
    UnityEvent OnDayChangedEvent;
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
        PostOffice.Subscribes(this, GameEvent.InGameUiStateEvent.ON_IN_GAME_UIS_STATE_CHANGED);
        dayData.ResetDay();
    }
    private void OnDestroy()
    {
        PostOffice.Unsubscribes(this, GameEvent.InGameUiStateEvent.ON_IN_GAME_UIS_STATE_CHANGED);
    }

    public int GetCurrentDay()
    {
        return dayData.currentDay;
    }

    public void NextDay()
    {
        OnDayChangedEvent.Invoke();
        SetCurrentDay(dayData.currentDay + 1);
        InGameUIControl.GetInstance().RequestState(InGameUIState.InGameUIStateEnum.NormalState);
    }

    private bool HasPublishedEnoughPaper()
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
        if (eventName == GameEvent.InGameUiStateEvent.ON_IN_GAME_UIS_STATE_CHANGED)
        {
            var newState = pack.GetValue<InGameUIState.InGameUIStateEnum>(GameEvent.InGameUiStateEvent.OnInGameUIsStateChangedData.NEW_STATE);
            if (newState == InGameUIState.InGameUIStateEnum.NormalState)
            {
                if (CanProceedToNextDay())
                {
                    NextDay();
                }
            }
        }
    }

    private bool CanProceedToNextDay()
    {
        return HasPublishedEnoughPaper() && publishedPaperData.paperDatas.Count >= dayData.amountOfPaperNeededPerDay[dayData.currentDay];
    }
}