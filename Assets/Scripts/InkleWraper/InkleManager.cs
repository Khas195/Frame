using System;
using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

public class InkleManager : SingletonMonobehavior<InkleManager>
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
            return descList[0];
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
}
