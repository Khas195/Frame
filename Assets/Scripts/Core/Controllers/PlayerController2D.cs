using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController2D : MonoBehaviour, IObserver
{
    [SerializeField]
    Character2D character = null;
    [SerializeField]
    UnityEvent interactTrigger = new UnityEvent();
    [SerializeField]
    [Required]
    GameObject playerEntity = null;
    [SerializeField]
    Rigidbody2D cameraBody;
    [SerializeField]
    Rigidbody2D playerBody;

    private void Awake()
    {
        PostOffice.Subscribes(this, GameEvent.PlayerEntityEvent.FETCH_PLAYER_ENTITY_EVENT);
        PostOffice.Subscribes(this, GameEvent.PlayerEntityEvent.NEW_STAGE_LOADED_EVENT);
    }
    private void OnDestroy()
    {
        PostOffice.Unsubscribes(this, GameEvent.PlayerEntityEvent.FETCH_PLAYER_ENTITY_EVENT);
        PostOffice.Unsubscribes(this, GameEvent.PlayerEntityEvent.NEW_STAGE_LOADED_EVENT);
    }
    // Update is called once per frame
    void Update()
    {
        var side = Input.GetAxisRaw("Horizontal");
        var forward = Input.GetAxisRaw("Vertical");
        if (Input.GetKeyDown(KeyCode.E))
        {
            interactTrigger.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            character.SwitchToRun();
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            character.SwitchToWalk();
        }
        character.Move(side, forward);
    }

    public void SetCharacter(Character2D targetCharacter)
    {
        character = targetCharacter;
    }

    public void ReceiveData(DataPack pack, string eventName)
    {
        if (eventName == GameEvent.PlayerEntityEvent.FETCH_PLAYER_ENTITY_EVENT)
        {
            pack.SetValue(GameEvent.PlayerEntityEvent.FetchPlayerEntityEventData.PLAYER_GAME_OBJECT, this.playerEntity);
        }
        else if (eventName == GameEvent.PlayerEntityEvent.NEW_STAGE_LOADED_EVENT)
        {
            var spawnPoint = pack.GetValue<Vector3>(GameEvent.PlayerEntityEvent.StageLoadedEventDAta.SPAWN_POINT);
            playerEntity.transform.position = spawnPoint;

        }
    }
    public void MoveWithCamera()
    {
        character.SetMovementTarget(cameraBody, true);
    }
    public void MoveWithCharacter()
    {
        character.SetMovementTarget(playerBody);
    }
}
