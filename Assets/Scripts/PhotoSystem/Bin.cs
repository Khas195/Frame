using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class Bin : MonoBehaviour
{
    [SerializeField]
    [ReadOnly]
    PhotoHolder toDiscardPhoto = null;
    public void OnPhotoEnter()
    {
        var photoHolder = NewsPaperPanel.GetInstance().GetCurrentSelection();
        if (photoHolder != null)
        {
            toDiscardPhoto = photoHolder;
        }
    }
    public void OnPhotoExit()
    {
        toDiscardPhoto = null;
    }
    public void OnPhotoDrop()
    {
        if (toDiscardPhoto != null)
        {
            NewsPaperPanel.GetInstance().TriggerDiscardPublishedPhotoEvent(toDiscardPhoto.GetPhotoInfo());
            toDiscardPhoto = null;
        }
    }
}
