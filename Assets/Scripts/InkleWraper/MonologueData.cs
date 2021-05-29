using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "MonologueData", menuName = "Data/MonologueData", order = 1)]
public class MonologueData : ScriptableObject
{
    public List<string> stitchesInStory = new List<string>();
    public List<MonologueLine> monologueLines;
    public void AddMonologueLines(List<MonologueLine> monologueLine, List<string> line)
    {
        for (int i = 0; i < line.Count; i++)
        {
            if (monologueLine.Find((MonologueLine x) => x.GetLine() == line[i]) == null)
            {
                monologueLine.Add(new MonologueLine(line[i], 0));
            }
        }
    }
    [Button]
    public void LoadLines()
    {
        InkleManager.GetInstance().CreateStory();
        for (int i = 0; i < stitchesInStory.Count; i++)
        {
            var newLines = InkleManager.GetInstance().GetCharacterPublishMonologues(stitchesInStory[i]);
            this.AddMonologueLines(monologueLines, newLines);

        }
        SortPiority();
    }
    [Button]
    public void ClearAll()
    {
        monologueLines.Clear();

    }
    [Button]
    public void SortPiority()
    {
        monologueLines.Sort((line1, line2) => line2.GetPiority().CompareTo(line1.GetPiority()));
    }
}
