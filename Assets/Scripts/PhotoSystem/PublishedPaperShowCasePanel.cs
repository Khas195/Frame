using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PublishedPaperShowCasePanel : MonoBehaviour, IObserver
{
    [SerializeField]
    Transform root;
    [SerializeField]
    PublishedPaper publishedPrefab;



    private void Awake()
    {
        PostOffice.Subscribes(this, GameEvent.NewspaperEvent.NEWSPAPER_PUBLISHED_EVENT);
    }



    private void OnDestroy()
    {

        PostOffice.Unsubscribes(this, GameEvent.NewspaperEvent.NEWSPAPER_PUBLISHED_EVENT);
    }
    public void Show()
    {
        this.root.gameObject.SetActive(true);
    }
    public void Hide()
    {
        this.root.gameObject.SetActive(false);
    }

    public void ReceiveData(DataPack pack, string eventName)
    {
        if (eventName == GameEvent.NewspaperEvent.NEWSPAPER_PUBLISHED_EVENT)
        {
            NewspaperData data = pack.GetValue<NewspaperData>(GameEvent.NewspaperEvent.PaperPublishedData.NEWSPAPER_DATA);
            if (data != null)
            {
                var newPublishedPaper = GameObject.Instantiate(publishedPrefab, root);

                newPublishedPaper.gameObject.SetActive(true);
                newPublishedPaper.SetData(data);
            }
        }
    }
}
