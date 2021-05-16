using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bin : MonoBehaviour
{
    [SerializeField]
    RectTransform photoArea;
    [SerializeField]
    List<PhotoHolder> discardList;
    PhotoHolder currentHolder = null;
    public void OnPhotoEnter()
    {
        var photoHolder = NewsPaperPanel.GetInstance().GetCurrentSelection();
        if (photoHolder != null)
        {
            currentHolder = photoHolder;
        }
    }
    public void OnPhotoExit()
    {
        if (currentHolder != null)
        {
            discardList.Remove(currentHolder);
        }
        currentHolder = null;
    }
    public void OnPhotoDrop()
    {
        if (currentHolder != null)
        {
            discardList.Add(currentHolder);
            currentHolder = null;
        }
    }
}
