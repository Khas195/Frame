using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewsPanelState : InGameUIState
{

    public override Enum GetEnum()
    {
        return InGameUIState.InGameUIStateEnum.NewsPanelState;
    }


    public override void OnStateEnter()
    {
        NewsPaperPanel.GetInstance().SwitchPanelOn();
    }

    public override void OnStateExit()
    {
        NewsPaperPanel.GetInstance().SwitchPanelOff();
    }

    public override void UpdateState()
    {
        if (Input.GetKeyDown(KeyCode.B) || Input.GetKeyDown(KeyCode.Escape))
        {
            this.controller.RequestState(InGameUIStateEnum.NormalState);
        }
        else if (Input.GetKeyDown(KeyCode.V))
        {
            controller.RequestState(InGameUIStateEnum.CapturingState);
        }

    }
    public void ExitState()
    {
        this.controller.RequestState(InGameUIStateEnum.NormalState);
    }
}
