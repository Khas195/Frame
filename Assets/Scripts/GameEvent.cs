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
    public static class DaySystemEvent
    {
        public static string DAY_CHANGED_EVENT = "DAY_CHANGED_EVENT";
        public static class OnDayChangedEventData
        {
            public static string CURRENT_DAY = "CURRENT_DAY";
        }
    }
    public static class PhotoEvent
    {
        public static string DISCARD_PHOTO_EVENT = "DISCARD_PHOTO_EVENT";
        public static class DiscardPhotoEventData
        {
            public static string PHOTO_INFOS = "PHOTO_INFOS";
        }
    }

}
