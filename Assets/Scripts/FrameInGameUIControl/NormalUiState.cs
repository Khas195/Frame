using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalUiState : InGameUIState
{
    public override Enum GetEnum()
    {
        return InGameUIStateEnum.NormalState;
    }

    public override void OnStateEnter()
    {
    }

    public override void OnStateExit()
    {
    }

    public override void UpdateState()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            controller.RequestState(InGameUIStateEnum.PhotoInventoryState);
        }
        else if (Input.GetKeyDown(KeyCode.V))
        {
            controller.RequestState(InGameUIStateEnum.CapturingState);
        }
        else if (Input.GetKeyDown(KeyCode.B))
        {
            controller.RequestState(InGameUIStateEnum.NewsPanelState);
        }
    }

}
