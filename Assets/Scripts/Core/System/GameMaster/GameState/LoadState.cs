using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class LoadState : GameState
{
	[SerializeField]
	List<GameInstance> gameMap = new List<GameInstance>();
	public override Enum GetEnum()
	{
		return GameStateEnum.LoadState;
	}

	public override void OnStateEnter()
	{
		var targetInstance = SceneLoadingManager.GetInstance().GetCurrentLoadInstance();
		var currentInstance = SceneLoadingManager.GetInstance().GetCurrentInstance();
		if (gameMap.Contains(targetInstance) && gameMap.Contains(currentInstance) && targetInstance != currentInstance)
		{
			SceneLoadingManager.GetInstance().LoadLoadingScene(true);
		}
		else
		{
			SceneLoadingManager.GetInstance().LoadLoadingScene(false);
		}
	}

	public override void OnStateExit()
	{
		SceneLoadingManager.GetInstance().FinishedLoadingProtocol();
	}

	public override void UpdateState()
	{
	}
}
