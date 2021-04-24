using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameEvent
{
    public static class CameraEvent
    {
        public static string ON_ENTER_CAMERA_MODE = "ON_ENTER_CAMERA_MODE";
        public static string ON_EXIT_CAMERA_MODE = "ON_EXIT_CAMERA_MODE";
        public static string ON_PHOTO_TAKEN = "ON_PHOTO_TAKEN";
        public static string TAKE_PHOTO_SIGNAL = "TAKE_PHOTO_SIGNAL";
        public static class PhotoTakenData
        {
            public static string PHOTO_INFO = "PHOTO_INFO";
        }
    }
}
