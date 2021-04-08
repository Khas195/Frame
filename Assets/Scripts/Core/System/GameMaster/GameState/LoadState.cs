using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadState : GameState
{
    public override Enum GetEnum()
    {
        return GameStateEnum.LoadState;
    }

    public override void OnStateEnter()
    {
        SceneLoadingManager.GetInstance().LoadLoadingScene();
    }

    public override void OnStateExit()
    {
        SceneLoadingManager.GetInstance().FinishedLoadingProtocol();
    }

    public override void UpdateState()
    {
        if (SceneLoadingManager.GetInstance().GetLoadingProgress() > 0.9f)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                LogHelper.Log("Scene finished loading!!");
                SceneLoadingManager.GetInstance().FinishedLoading();
            }
        }
    }
}
