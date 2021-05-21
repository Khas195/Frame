using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewsokiNewspaperShowCase : MonoBehaviour, IObserver
{
    [SerializeField]
    PublishedPapersData publishedPapersData = null;
    [SerializeField]
    Transform newPapersRoot;
    [SerializeField]
    Transform oldPapersRoot;
    [SerializeField]
    List<NewspaperShowCaseAfterPrint> newsPapers;
    [SerializeField]
    List<NewspaperShowCaseAfterPrint> oldPapers;
    private void Awake()
    {
        PostOffice.Subscribes(this, GameEvent.NewspaperEvent.NEWSPAPER_PUBLISHED_EVENT);
    }
    private void OnDestroy()
    {
        PostOffice.Unsubscribes(this, GameEvent.NewspaperEvent.NEWSPAPER_PUBLISHED_EVENT);
    }
    // Start is called before the first frame update
    void Start()
    {
        newsPapers.AddRange(newPapersRoot.GetComponentsInChildren<NewspaperShowCaseAfterPrint>());
        oldPapers.AddRange(oldPapersRoot.GetComponentsInChildren<NewspaperShowCaseAfterPrint>());
        UpdateShopShowCaseState();
    }

    private void UpdateShopShowCaseState()
    {
        int curPublishedPaper = 0;
        curPublishedPaper = SetPublishedPaperToNewPapers(curPublishedPaper);
        curPublishedPaper = SetPublishedPaperToOldPapers(curPublishedPaper);
        SetActiveStateForNewPapers();
        SetActiveStateForOldPapers();
    }

    private int SetPublishedPaperToOldPapers(int curPublishedPaper)
    {
        for (int i = 0; i < oldPapers.Count; i++, curPublishedPaper++)
        {
            if (curPublishedPaper >= publishedPapersData.paperDatas.Count)
            {
                return curPublishedPaper;
            }
            else
            {
                oldPapers[i].PrintPhotos(publishedPapersData.paperDatas[curPublishedPaper], false);
            }
        }

        return curPublishedPaper;
    }

    private int SetPublishedPaperToNewPapers(int curPublishedPaper)
    {
        for (int i = 0; i < newsPapers.Count; i++, curPublishedPaper++)
        {
            if (curPublishedPaper >= publishedPapersData.paperDatas.Count)
            {
                return curPublishedPaper;
            }
            else
            {
                newsPapers[i].PrintPhotos(publishedPapersData.paperDatas[curPublishedPaper], false);
            }
        }

        return curPublishedPaper;
    }

    private void SetActiveStateForOldPapers()
    {
        for (int i = 0; i < oldPapers.Count; i++)
        {
            if (oldPapers[i].HasData() == false)
            {
                oldPapers[i].gameObject.SetActive(false);
            }
            else
            {
                oldPapers[i].gameObject.SetActive(true);
            }
        }
    }

    private void SetActiveStateForNewPapers()
    {
        for (int i = 0; i < newsPapers.Count; i++)
        {
            if (newsPapers[i].HasData() == false)
            {
                newsPapers[i].gameObject.SetActive(false);
            }
            else
            {
                newsPapers[i].gameObject.SetActive(true);
            }
        }
    }

    public void ReceiveData(DataPack pack, string eventName)
    {
        if (eventName.Equals(GameEvent.NewspaperEvent.NEWSPAPER_PUBLISHED_EVENT))
        {
            UpdateShopShowCaseState();
        }
    }
}
