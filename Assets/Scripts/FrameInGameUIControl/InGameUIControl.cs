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
        manager.RequestState(newState);
    }
}

