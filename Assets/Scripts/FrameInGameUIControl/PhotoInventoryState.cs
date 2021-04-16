using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotoInventoryState : InGameUIState
{
    [SerializeField]
    PhotoListManager inventory = null;
    public override Enum GetEnum()
    {
        return InGameUIStateEnum.PhotoInventoryState;
    }

    public override void OnStateEnter()
    {
        inventory.Show();
    }

    public override void OnStateExit()
    {
        inventory.Hide();
    }

    public override void UpdateState()
    {
        if (Input.GetKeyDown(KeyCode.Tab) || Input.GetKeyDown(KeyCode.Escape))
        {
            this.controller.RequestState(InGameUIStateEnum.NormalState);
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
