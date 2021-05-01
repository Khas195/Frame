using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PublishedPaperShowCasePanel : MonoBehaviour, IObserver
{
    [SerializeField]
    Transform panel;
    [SerializeField]
    Transform root;
    [SerializeField]
    PublishedPaper publishedPrefab;
    [SerializeField]
    FadeManyTransition fade;
    [SerializeField]
    SwayStatsPanel swayStatsPanel;
    [SerializeField]
    List<PublishedPaper> publishedPaperInReview = new List<PublishedPaper>();


    private void Awake()
    {
        PostOffice.Subscribes(this, GameEvent.NewspaperEvent.NEWSPAPER_PUBLISHED_EVENT);
    }
    private void OnDestroy()
    {
        PostOffice.Unsubscribes(this, GameEvent.NewspaperEvent.NEWSPAPER_PUBLISHED_EVENT);
    }
    public void NextDay()
    {
        for (int i = publishedPaperInReview.Count - 1; i >= 0; i--)
        {
            Destroy(publishedPaperInReview[i].gameObject);
        }
        publishedPaperInReview.Clear();
        DaySystem.GetInstance().NextDay();
    }
    public void Show()
    {
        this.panel.gameObject.SetActive(true);
        fade.FadeIn(() =>
        {
            swayStatsPanel.TriggerSliderUpdate();
        });
    }
    public void Hide()
    {
        fade.FadeOut(() =>
        {
            this.panel.gameObject.SetActive(false);
        });
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
                publishedPaperInReview.Add(newPublishedPaper);
            }
        }
    }
}
