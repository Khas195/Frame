using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animator2DController : MonoBehaviour, IObserver
{
    [SerializeField]
    Animator characterAnim = null;
    [SerializeField]
    Rigidbody2D characterBody = null;
    [SerializeField]
    IMovement currentMovement = null;

    private void Start()
    {
        PostOffice.Subscribes(this, GameEvent.InGameUiStateEvent.ON_IN_GAME_UIS_STATE_CHANGED);
    }
    private void OnDestroy()
    {
        PostOffice.Unsubscribes(this, GameEvent.InGameUiStateEvent.ON_IN_GAME_UIS_STATE_CHANGED);
    }

    // Update is called once per frame
    void Update()
    {
        characterAnim.SetFloat("Speed", characterBody.simulated == true ? characterBody.velocity.magnitude : 0);
        if (currentMovement != null)
        {
            if (currentMovement.GetCurrentMoveMode() == IMovement.MovementType.Run)
            {
                characterAnim.SetBool("IsRunning", true);
            }
            else
            {
                characterAnim.SetBool("IsRunning", false);
            }
        }

    }
    public void ReceiveData(DataPack pack, string eventName)
    {
        if (eventName == GameEvent.InGameUiStateEvent.ON_IN_GAME_UIS_STATE_CHANGED)
        {
            var newState = pack.GetValue<InGameUIState.InGameUIStateEnum>(GameEvent.InGameUiStateEvent.OnInGameUIsStateChangedData.NEW_STATE);
            if (newState == InGameUIState.InGameUIStateEnum.CapturingState)
            {
                characterAnim.SetBool("IsInCameraMode", true);
            }
            else
            {
                characterAnim.SetBool("IsInCameraMode", false);
            }
        }
    }
}

