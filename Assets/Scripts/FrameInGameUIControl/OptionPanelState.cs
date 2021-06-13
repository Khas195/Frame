using System;
using UnityEngine;

public class OptionPanelState : InGameUIState
{
	[SerializeField]
	GameObject optionPanelRoot = null;
	public override Enum GetEnum()
	{
		return InGameUIState.InGameUIStateEnum.OptionState;
	}
	public override void OnStateEnter()
	{
		optionPanelRoot.gameObject.SetActive(true);
		GameMaster.GetInstance().FreezeGame();
	}
	public override void OnStateExit()
	{
		optionPanelRoot.gameObject.SetActive(false);
		GameMaster.GetInstance().UnFreezeGame();
	}
	public override void UpdateState()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			controller.RequestState(InGameUIStateEnum.NormalState);
		}
	}
}
