using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MonologueLine
{
    [SerializeField]
    string line;
    [SerializeField]
    float showTime = 0.0f;
    [SerializeField]
    int piority = 0;
    [SerializeField]
    List<MonologueLineCondition> conditions = new List<MonologueLineCondition>();

    public MonologueLine(string line, int showTime)
    {
        this.line = line;
        this.showTime = showTime;
    }
    public bool IsAllConditionSatisfied()
    {
        if (conditions.Count <= 0) return true;
        for (int i = 0; i < conditions.Count; i++)
        {
            if (conditions[i].IsSatisfied() == false)
            {
                return false;
            }
        }
        return true;
    }
    public int GetPiority()
    {
        return piority;
    }
    public float GetShowTime()
    {
        return showTime;
    }
    public string GetLine()
    {
        return line;
    }

}
