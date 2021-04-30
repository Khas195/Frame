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
    }
}
