using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotoCapturingState : InGameUIState
{
    [SerializeField]
    DragSelection selection;
    [SerializeField]
    TakePicture takePhoto;
    public override Enum GetEnum()
    {
        return InGameUIStateEnum.CapturingState;
    }

    public override void OnStateEnter()
    {
        selection.EnterCameraMode();
    }

    public override void OnStateExit()
    {
        selection.ExitCameraMode();
    }

    public override void UpdateState()
    {
        if (Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(0))
        {
            if (takePhoto.IsCameraReady())
            {

                takePhoto.TakePhoto();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.V))
        {
            this.controller.RequestState(InGameUIStateEnum.NormalState);
        }
        else if (Input.GetKeyDown(KeyCode.Tab))
        {
            controller.RequestState(InGameUIStateEnum.PhotoInventoryState);
        }

        else if (Input.GetKeyDown(KeyCode.B))
        {
            controller.RequestState(InGameUIStateEnum.NewsPanelState);
        }
    }
    public void GoToPhotoMode()
    {
        this.controller.RequestState(InGameUIStateEnum.PhotoMode);
    }
}
