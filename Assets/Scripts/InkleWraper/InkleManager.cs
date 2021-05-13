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
    TextAsset gameStoryAsset;
    [SerializeField]
    [ReadOnly]
    Story story;
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

        story = new Story(gameStoryAsset.text);
    }

    public void AddPaperboyLines(string inkleStitch)
    {
        paperboysLines.AddRange(GetLinesFromSticth(inkleStitch));
    }
    public List<string> GetLinesFromSticth(string stitch)
    {
        var result = new List<string>();
        story.ChoosePathString(stitch);
        story.Continue();
        for (int i = 0; i < story.currentChoices.Count; i++)
        {
            result.Add(story.currentChoices[i].text);
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
