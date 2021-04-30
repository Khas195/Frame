using System;
using UnityEngine;

public class PublishedPanelState : InGameUIState
{
    [SerializeField]
    PublishedPaperShowCasePanel panel;
    public override Enum GetEnum()
    {
        return InGameUIState.InGameUIStateEnum.PublishedPaperPanel;
    }

    public override void OnStateEnter()
    {
        panel.Show();
    }

    public override void OnStateExit()
    {
        panel.Hide();
    }

    public override void UpdateState()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.T))
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
}
