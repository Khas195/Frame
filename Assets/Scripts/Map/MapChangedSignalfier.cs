using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapChangedSignalfier : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PostOffice.SendData(null, GameEvent.MapChangedEvent.MAP_CHANGED_EVENT);
    }

}
