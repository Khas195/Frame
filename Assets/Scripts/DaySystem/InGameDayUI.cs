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
    private void Start()
    {
        var currentDay = DaySystem.GetInstance().GetCurrentDay();
        if (currentDay > 0)
        {
            UpdateDayTextUI(currentDay, dayTextUI01);
            currentDayUI = dayTextUI01;
            SwitchDayText();
            UpdateDayTextUI(currentDay + 1, currentDayUI);
        }
    }

    public void ReceiveData(DataPack pack, string eventName)
    {
        if (eventName.Equals(GameEvent.DaySystemEvent.DAY_CHANGED_EVENT))
        {
            int currentDay = pack.GetValue<int>(GameEvent.DaySystemEvent.OnDayChangedEventData.CURRENT_DAY);
            currentDay += 1;
            SwitchDayText();

            UpdateDayTextUI(currentDay, currentDayUI);
        }
    }

    private void SwitchDayText()
    {
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
    }

    private void UpdateDayTextUI(int currentDay, Text textUI)
    {
        if (currentDay < 10)
        {
            textUI.text = "0" + currentDay.ToString();
        }
        else
        {
            textUI.text = currentDay.ToString();
        }
    }
}
