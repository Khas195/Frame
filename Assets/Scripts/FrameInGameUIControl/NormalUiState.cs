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
        if (Input.GetKeyDown(KeyCode.E))
        {
            controller.RequestState(InGameUIStateEnum.CapturingState);
        }
        else if (Input.GetKeyDown(KeyCode.B) && allowedDirectTransitionToNewspanel)
        {
            controller.RequestState(InGameUIStateEnum.NewsPanelState);
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            controller.RequestState(InGameUIStateEnum.DiaryPanelState);
        }
        if (GameMaster.GetInstance(forceCreate: false))
        {
            if ((GameState.GameStateEnum)GameMaster.GetInstance().GetCurrentState().GetEnum() == GameState.GameStateEnum.LoadState)
            {
                overlayUI.gameObject.SetActive(false);
            }
            else
            {
                overlayUI.gameObject.SetActive(true);
            }
        }
    }

}
