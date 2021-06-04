using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapChangedSignalfier : MonoBehaviour
{
    [SerializeField]
    GameEvent.MapChangedEvent.MapLocation currentLocation;
    // Start is called before the first frame update
    void Start()
    {
        var data = DataPool.GetInstance().RequestInstance();
        data.SetValue(GameEvent.MapChangedEvent.MAP_LOCATION_DATA, currentLocation);
        PostOffice.SendData(data, GameEvent.MapChangedEvent.MAP_CHANGED_EVENT);
        DataPool.GetInstance().ReturnInstance(data);
    }

}
