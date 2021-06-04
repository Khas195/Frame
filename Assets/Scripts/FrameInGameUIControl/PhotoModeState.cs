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
    bool entertingOtherUI = false;

    public override void UpdateState()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            controller.RequestState(InGameUIStateEnum.CapturingState);
            entertingOtherUI = true;
        }

        if (Input.anyKeyDown && entertingOtherUI == false)
        {
            this.controller.RequestState(InGameUIStateEnum.NormalState);
        }
    }
}
