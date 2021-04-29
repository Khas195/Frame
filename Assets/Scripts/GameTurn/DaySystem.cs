using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaySystem : SingletonMonobehavior<DaySystem>
{
    [SerializeField]
    int currentDay = 0;
    public void NextDay()
    {
        currentDay += 1;
        TriggerOnDayChangedEvent();

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
}
