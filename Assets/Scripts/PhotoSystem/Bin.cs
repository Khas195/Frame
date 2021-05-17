using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

public class Bin : MonoBehaviour
{
    [SerializeField]
    Image trayImage;
    [SerializeField]
    Sprite trayNormal;
    [SerializeField]
    Sprite trayHighlight;


    [SerializeField]
    [ReadOnly]
    PhotoHolder toDiscardPhoto = null;
    [SerializeField]
    GameObject crumbsArea = null;
    [SerializeField]
    GameObject crumbsPrefab = null;
    [SerializeField]
    AudioSource paperCrumbles;
    [SerializeField]
    [ReadOnly]
    List<GameObject> crumbsList = new List<GameObject>();

    public void OnPhotoEnter()
    {
        var photoHolder = NewsPaperPanel.GetInstance().GetCurrentSelection();
        if (photoHolder != null)
        {
            toDiscardPhoto = photoHolder;
            trayImage.sprite = trayHighlight;
        }
        else
        {
            trayImage.sprite = trayNormal;
        }
    }
    public void OnPhotoExit()
    {
        toDiscardPhoto = null;
        trayImage.sprite = trayNormal;
    }
    public void OnPhotoDrop()
    {
        if (toDiscardPhoto != null)
        {
            NewsPaperPanel.GetInstance().TriggerDiscardPublishedPhotoEvent(toDiscardPhoto.GetPhotoInfo());
            toDiscardPhoto = null;
            trayImage.sprite = trayNormal;
            var newCrumb = GameObject.Instantiate(crumbsPrefab, crumbsArea.transform);
            newCrumb.transform.position = Input.mousePosition;
            newCrumb.gameObject.SetActive(true);
            crumbsList.Add(newCrumb);
            paperCrumbles.Play();
        }
    }
    private void OnDisable()
    {
        for (int i = 0; i < crumbsList.Count; i++)
        {
            Destroy(crumbsList[i]);
        }
        crumbsList.Clear();
    }
}
