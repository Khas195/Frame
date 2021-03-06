using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class InGameUIControl : SingletonMonobehavior<InGameUIControl>
{
    [SerializeField]
    [Required]
    StateManager manager = null;
    private void Start()
    {
        manager.RequestState(InGameUIState.InGameUIStateEnum.NormalState);
        var story = InkleManager.GetInstance().GetPlayerConversation();
        LogHelper.Log("Inkle Intergration: Bind Open Taxi Map");
        story.BindExternalFunction("OpenTaxiMap", () =>
        {
            this.RequestState(InGameUIState.InGameUIStateEnum.MapState);
        });
        LogHelper.Log("Inkle Intergration: Bind Open Publish  Panel");
        story.BindExternalFunction("OpenNewsPanel", () =>
        {
            this.RequestState(InGameUIState.InGameUIStateEnum.NewsPanelState);
        });



    }
    private void Update()
    {
        var currentState = manager.GetCurrentState<InGameUIState>();
        if (currentState != null)
        {
            currentState.UpdateState();
        }
    }

    public void RequestState(InGameUIState.InGameUIStateEnum newState)
    {
        if (manager.RequestState(newState))
        {
            var data = DataPool.GetInstance().RequestInstance();
            data.SetValue(GameEvent.InGameUiStateEvent.OnInGameUIsStateChangedData.NEW_STATE, newState);
            PostOffice.SendData(data, GameEvent.InGameUiStateEvent.ON_IN_GAME_UIS_STATE_CHANGED);
            DataPool.GetInstance().ReturnInstance(data);
        }
    }

    public InGameUIState.InGameUIStateEnum GetCurrentState()
    {
        return (InGameUIState.InGameUIStateEnum)manager.GetCurrentState<InGameUIState>().GetEnum();
    }
}

