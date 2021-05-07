using System;
using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;
using NaughtyAttributes;
using UnityEngine;

public class InkleManager : SingletonMonobehavior<InkleManager>
{
    [SerializeField]
    TextAsset paperBoyDialogues;
    Story paperboyStory;
    [SerializeField]
    [ReadOnly]
    List<string> paperboysLines = new List<string>();
    protected override void Awake()
    {
        base.Awake();
        paperboyStory = new Story(paperBoyDialogues.text);
    }

    public void AddPaperboyLines(string inkleStitch)
    {
        paperboyStory.ChoosePathString(inkleStitch);
        paperboyStory.Continue();
        for (int i = 0; i < paperboyStory.currentChoices.Count; i++)
        {
            paperboysLines.Add(paperboyStory.currentChoices[i].text);
        }
    }

    public List<string> GetGenericPaperLines()
    {
        var result = new List<string>();
        paperboyStory.ChoosePathString("PaperboysLine.GenericLines");
        paperboyStory.Continue();
        for (int i = 0; i < paperboyStory.currentChoices.Count; i++)
        {
            result.Add(paperboyStory.currentChoices[i].text);
        }
        return result;
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
