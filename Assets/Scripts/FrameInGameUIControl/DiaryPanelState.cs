using System;
using NaughtyAttributes;
using UnityEngine;

public class DiaryPanelState : InGameUIState
{
    [SerializeField]
    [Required]
    DiaryPanel panel;
    public override Enum GetEnum()
    {
        return InGameUIStateEnum.DiaryPanelState;
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
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Q))
        {
            InGameUIControl.GetInstance().RequestState(InGameUIStateEnum.NormalState);
        }
    }
}
