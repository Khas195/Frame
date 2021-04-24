using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameEvent
{
    public static class PlayerEntityEvent
    {
        public static string FETCH_PLAYER_ENTITY_EVENT = "FETCH_PLAYER_ENTITY_EVENT";
        public static class FetchPlayerEntityEventData
        {
            public static string PLAYER_GAME_OBJECT = "PLAYER_GAME_OBJECT";
        }
    }

}
