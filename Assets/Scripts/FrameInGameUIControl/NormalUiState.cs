using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalUiState : InGameUIState
{
    [SerializeField]
    GameObject overlayUI;
    public override Enum GetEnum()
    {
        return InGameUIStateEnum.NormalState;
    }

    public override void OnStateEnter()
    {
        overlayUI.gameObject.SetActive(true);
    }

    public override void OnStateExit()
    {
        overlayUI.gameObject.SetActive(false);
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
        else if (Input.GetKeyDown(KeyCode.T))
        {
            controller.RequestState(InGameUIStateEnum.PublishedPaperPanel);
        }
    }

}
