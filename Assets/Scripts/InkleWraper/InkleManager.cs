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
    [ReadOnly]
    Story monologueStory;



    [SerializeField]
    [ReadOnly]
    Story playerConversationStory;
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
    }
    public Story GetPlayerConversation()
    {
        return playerConversationStory;
    }
    public void AddPaperboyLines(string inkleStitch)
    {
        paperboysLines.AddRange(GetLinesFromSticth(inkleStitch));
    }
    public List<string> GetLinesFromSticth(string stitch)
    {
        var result = new List<string>();
        monologueStory.ChoosePathString(stitch);
        monologueStory.Continue();
        for (int i = 0; i < monologueStory.currentChoices.Count; i++)
        {
            result.Add(monologueStory.currentChoices[i].text);
        }
        return result;
    }

    public List<string> GetGenericPaperLines()
    {
        return GetLinesFromSticth("PaperboysLine.GenericLines");
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
