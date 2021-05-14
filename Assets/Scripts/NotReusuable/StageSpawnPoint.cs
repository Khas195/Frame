using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSpawnPoint : MonoBehaviour
{
    void Start()
    {
        var data = DataPool.GetInstance().RequestInstance();
        data.SetValue(GameEvent.PlayerEntityEvent.StageLoadedEventDAta.SPAWN_POINT, this.transform.position);
        PostOffice.SendData(data, GameEvent.PlayerEntityEvent.NEW_STAGE_LOADED_EVENT);
        DataPool.GetInstance().ReturnInstance(data);
    }
}
