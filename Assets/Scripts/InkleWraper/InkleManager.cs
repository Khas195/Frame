using System;
using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

public class InkleManager : SingletonMonobehavior<InkleManager>, IObserver
{
    [SerializeField]
    TextAsset monologues;
    [SerializeField]
    TextAsset playerConversations;
    [SerializeField]
    TextAsset actorDescriptions;
    [SerializeField]
    [ReadOnly]
    Story monologueStory;



    [SerializeField]
    [ReadOnly]
    Story playerConversationStory;
    [SerializeField]
    [ReadOnly]
    Story actorDescriptionsStory;
    [SerializeField]
    [ReadOnly]
    List<string> paperboysLines = new List<string>();
    protected override void Awake()
    {
        base.Awake();
        CreateStory();
        PostOffice.Subscribes(this, GameEvent.NewspaperEvent.NEWSPAPER_PUBLISHED_EVENT);
    }
    private void OnDestroy()
    {
        PostOffice.Unsubscribes(this, GameEvent.NewspaperEvent.NEWSPAPER_PUBLISHED_EVENT);
    }
    [Button]
    public void CreateStory()
    {
        monologueStory = new Story(monologues.text);
        playerConversationStory = new Story(playerConversations.text);
        actorDescriptionsStory = new Story(actorDescriptions.text);
    }

    public Story GetPlayerConversation()
    {
        return playerConversationStory;
    }
    public void AddPaperboyLines(string inkleStitch)
    {
        paperboysLines.AddRange(GetLinesFromSticth(inkleStitch, monologueStory));
    }
    public List<string> GetCharacterPublishMonologues(string inkleStitch)
    {
        return GetLinesFromSticth(inkleStitch, monologueStory);
    }
    public List<string> GetLinesFromSticth(string stitch, Story StichStory)
    {
        var result = new List<string>();
        StichStory.ChoosePathString(stitch);
        StichStory.Continue();
        for (int i = 0; i < StichStory.currentChoices.Count; i++)
        {
            result.Add(StichStory.currentChoices[i].text);
        }
        return result;
    }



    public List<string> GetGenericPaperLines()
    {
        return GetLinesFromSticth("PaperboysLine.GenericLines", monologueStory);
    }

    public string RequestActorDesc(string stitch)
    {
        var descList = GetLinesFromSticth(stitch, actorDescriptionsStory);
        if (descList.Count > 0)
        {
            var randomedLine = descList[UnityEngine.Random.Range(0, descList.Count)];
            return randomedLine;
        }
        else
        {
            return "";
        }
    }

    public void ClearPaperboyLines()
    {
        paperboysLines.Clear();
    }

    public List<string> RequestTodayLines()
    {
        return paperboysLines;
    }

    public void SetVariable<T>(string variableName, T value)
    {
        this.playerConversationStory.variablesState[variableName] = value;
        LogHelper.Log("Inkle Manager - Setting Variable: " + variableName + " with value " + value);
    }
    public T GetVariable<T>(string variableName)
    {
        var result = this.playerConversationStory.variablesState[variableName];
        LogHelper.Log("Inkle Manager - Getting Variable: " + variableName + " with result " + (T)result);
        return (T)result;
    }

    public void ReceiveData(DataPack pack, string eventName)
    {
        if (eventName == GameEvent.NewspaperEvent.NEWSPAPER_PUBLISHED_EVENT)
        {
            var commiePoint = pack.GetValue<int>(GameEvent.NewspaperEvent.PaperPublishedData.TOTAL_COMMIE_POINT);
            var capitalPoint = pack.GetValue<int>(GameEvent.NewspaperEvent.PaperPublishedData.TOTAL_CAPITAL_POINT);
            this.SetVariable("newPaperCommiePoint", commiePoint);
            this.SetVariable("newPaperCapitalPoint", capitalPoint);
        }
    }
}
