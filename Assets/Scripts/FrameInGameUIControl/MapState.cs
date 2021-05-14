using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapState : InGameUIState
{
    [SerializeField]
    TaxiMapControl mapStatePanel = null;
    public override Enum GetEnum()
    {
        return InGameUIStateEnum.MapState;
    }

    public override void OnStateEnter()
    {
        mapStatePanel.ShowMap();
    }

    public override void OnStateExit()
    {
        mapStatePanel.HideMap();
    }

    public override void UpdateState()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            controller.RequestState(InGameUIStateEnum.NormalState);
        }
    }
}
