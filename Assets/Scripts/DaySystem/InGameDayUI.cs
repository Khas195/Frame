using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

public class InGameDayUI : MonoBehaviour, IObserver
{
    [SerializeField]
    Text dayTextUI01 = null;
    [SerializeField]
    FadeTransition transition01 = null;
    [SerializeField]
    Text dayTextUI02 = null;
    [SerializeField]
    FadeTransition transition02 = null;
    [SerializeField]
    [ReadOnly]
    Text currentDayUI = null;
    private void Awake()
    {
        PostOffice.Subscribes(this, GameEvent.DaySystemEvent.DAY_CHANGED_EVENT);
    }
    private void OnDestroy()
    {
        PostOffice.Unsubscribes(this, GameEvent.DaySystemEvent.DAY_CHANGED_EVENT);
    }
    public void ReceiveData(DataPack pack, string eventName)
    {
        if (eventName.Equals(GameEvent.DaySystemEvent.DAY_CHANGED_EVENT))
        {
            int currentDay = pack.GetValue<int>(GameEvent.DaySystemEvent.OnDayChangedEventData.CURRENT_DAY);
            currentDay += 1;
            if (currentDayUI == null || currentDayUI == dayTextUI02)
            {
                currentDayUI = dayTextUI01;
                transition01.FadeIn();
                transition02.FadeOut();
            }
            else
            {
                currentDayUI = dayTextUI02;
                transition02.FadeIn();
                transition01.FadeOut();
            }

            UpdateDayTextUI(currentDay, currentDayUI);
        }
    }


    private void UpdateDayTextUI(int currentDay, Text textUI)
    {
        if (currentDay < 10)
        {
            textUI.text = "Day " + "0" + currentDay;
        }
        else
        {
            textUI.text = "Day " + currentDay;
        }
    }
}
