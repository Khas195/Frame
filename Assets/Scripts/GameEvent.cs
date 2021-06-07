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
        public static string NEW_STAGE_LOADED_EVENT = "NEW_STAGE_LOADED_EVENT";
        public static class StageLoadedEventDAta
        {
            public static string SPAWN_POINT = "SPAWN_POINT";
        }
    }
    public static class MapChangedEvent
    {
        public enum MapLocation
        {
            MarestromPark,
            PicisStreet
        }
        public static string MAP_CHANGED_EVENT = "MAP_CHANGED_EVENT";
        public static string MAP_LOCATION_DATA = "MAP_LOCATION_DATA";
    }
    public static class InGameUiStateEvent
    {
        public static string ON_IN_GAME_UIS_STATE_CHANGED = "ON_IN_GAME_UIS_STATE_CHANGED";
        public static class OnInGameUIsStateChangedData
        {
            public static string NEW_STATE = "NEW_STATE";
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
    public static class NewspaperEvent
    {
        public static string NEWSPAPER_PUBLISHED_EVENT = "NEWSPAPER_PUBLISHED_EVENT";
        public static class PaperPublishedData
        {
            public static string NEWSPAPER_DATA = "NEWSPAPER_DATA";
            public static string TOTAL_COMMIE_POINT = "TOTAL_COMMIE_POINT";
            public static string TOTAL_CAPITAL_POINT = "TOTAL_CAPITAL_POINT";
        }
    }
}
