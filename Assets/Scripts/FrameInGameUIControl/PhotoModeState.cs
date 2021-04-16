using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotoModeState : InGameUIState
{
    [SerializeField]
    DragSelection selection;
    public override Enum GetEnum()
    {
        return InGameUIStateEnum.PhotoMode;
    }

    public override void OnStateEnter()
    {
        selection.EnterPhotoMode();
    }

    public override void OnStateExit()
    {
        selection.ExitPhotoMode();
    }

    public override void UpdateState()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            this.controller.RequestState(InGameUIStateEnum.NormalState);
        }
        else if (Input.GetKeyDown(KeyCode.Tab))
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
