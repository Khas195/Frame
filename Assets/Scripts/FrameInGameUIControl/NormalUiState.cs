using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalUiState : InGameUIState
{
    [SerializeField]
    GameObject overlayUI;
    [SerializeField]
    bool allowedDirectTransitionToNewspanel = true;
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
    [SerializeField]
    GameInstance testInstance;

    public override void UpdateState()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            controller.RequestState(InGameUIStateEnum.CapturingState);
        }
        else if (Input.GetKeyDown(KeyCode.B) && allowedDirectTransitionToNewspanel)
        {
            controller.RequestState(InGameUIStateEnum.NewsPanelState);
        }
    }

}
