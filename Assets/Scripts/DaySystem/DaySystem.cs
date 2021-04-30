using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DaySystem : SingletonMonobehavior<DaySystem>, IObserver
{
    [SerializeField]
    int currentDay = 0;
    [SerializeField]
    DaySystemData dayData;
    [SerializeField]
    GameObject nextDayButton = null;
    [SerializeField]
    Text nextDayText = null;
    [SerializeField]
    FadeTransition textFadeTrans = null;
    int amountOfPublishedPaperToday = 0;


    protected override void Awake()
    {
        PostOffice.Subscribes(this, GameEvent.NewspaperEvent.NEWSPAPER_PUBLISHED_EVENT);
    }
    private void OnDestroy()
    {
        PostOffice.Unsubscribes(this, GameEvent.NewspaperEvent.NEWSPAPER_PUBLISHED_EVENT);
    }
    private void Update()
    {
        if (nextDayButton.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                NextDay();
                textFadeTrans.FadeOut();
            }
        }
    }
    public void NextDay()
    {
        if (IsPossibleToProceedToNextDay())
        {
            SetCurrentDay(currentDay + 1);
        }
    }

    private bool IsPossibleToProceedToNextDay()
    {
        return currentDay < dayData.amountOfPaperNeededPerDay.Count;
    }

    public void SetCurrentDay(int newDay)
    {
        currentDay = newDay;
        TriggerOnDayChangedEvent();
    }

    private void TriggerOnDayChangedEvent()
    {
        var data = DataPool.GetInstance().RequestInstance();
        data.SetValue(GameEvent.DaySystemEvent.OnDayChangedEventData.CURRENT_DAY, currentDay);
        PostOffice.SendData(data, GameEvent.DaySystemEvent.DAY_CHANGED_EVENT);
        DataPool.GetInstance().ReturnInstance(data);
    }

    public void ReceiveData(DataPack pack, string eventName)
    {
        if (eventName == GameEvent.NewspaperEvent.NEWSPAPER_PUBLISHED_EVENT)
        {
            amountOfPublishedPaperToday += 1;
            if (amountOfPublishedPaperToday >= dayData.amountOfPaperNeededPerDay[currentDay] && IsPossibleToProceedToNextDay())
            {
                textFadeTrans.FadeIn();
            }
        }
    }

}