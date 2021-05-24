using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;
public class CharacterPublishMonologue : MonoBehaviour
{
    [SerializeField]
    List<string> stitchesInStory = new List<string>();

    [SerializeField]
    List<MonologueLine> monologueLines;
    [SerializeField]
    [ReadOnly]
    MonologueLine lastSpokenLine = null;

    [SerializeField]
    FadeManyTransition fadeManyTransition = null;
    [SerializeField]
    Text textUI;
    [SerializeField]
    [MinMaxSlider(1, 10)]
    Vector2 restTime;
    [SerializeField]
    [ReadOnly]
    float curShowTime = 0;
    [SerializeField]
    [ReadOnly]
    float targetRestTime = 0;
    [SerializeField]
    [ReadOnly]
    float curRestTime = 100f;
    [SerializeField]
    [ReadOnly]
    MonologueLine currentLine = null;
    [SerializeField]
    float letterPause = 0.02f;
    [SerializeField]
    [ReadOnly]
    string currentMessage = "";
    bool isSpeaking = false;
    bool isResting = false;
    [SerializeField]
    List<MonologueLine> possibleLineToSpeak;
    // Start is called before the first frame update


    private void Start()
    {
        SortPiority();
    }
    // Update is called once per frame
    void Update()
    {
        if (isSpeaking == false && isResting == false)
        {
            Speak();
        }
        else
        {
            if (isSpeaking)
            {
                if (curShowTime <= currentLine.GetShowTime())
                {
                    curShowTime += Time.deltaTime;
                }
                else
                {
                    StartResting();
                }
            }
            else if (isResting)
            {
                if (curRestTime <= targetRestTime)
                {
                    curRestTime += Time.deltaTime;
                }
                else
                {
                    isResting = false;
                }
            }

        }
    }

    private void StartResting()
    {
        fadeManyTransition.FadeOut();
        curRestTime = 0;
        targetRestTime = UnityEngine.Random.Range(restTime.x, restTime.y);
        isSpeaking = false;
        isResting = true;
    }

    private void Speak()
    {
        var possibleLines = new List<MonologueLine>();
        possibleLines.AddRange(this.monologueLines.FindAll((x) => x.IsAllConditionSatisfied() == true && x != lastSpokenLine));
        possibleLines = FindAllElementsWithHighestPiority(possibleLines);
        possibleLineToSpeak = possibleLines;
        if (possibleLines.Count > 0)
        {
            currentLine = possibleLines[UnityEngine.Random.Range(0, possibleLines.Count)];
        }
        else
        {
            currentLine = null;
        }
        lastSpokenLine = currentLine;

        ShowLine(currentLine);
    }


    private List<MonologueLine> FindAllElementsWithHighestPiority(List<MonologueLine> possibleLines)
    {
        var result = new List<MonologueLine>();
        var highestPiority = 0;

        for (int i = 0; i < possibleLines.Count; i++)
        {
            if (possibleLines[i].GetPiority() >= highestPiority)
            {
                highestPiority = possibleLines[i].GetPiority();
                result.Add(possibleLines[i]);
            }
        }

        return result;
    }

    private void ShowLine(MonologueLine currentLine)
    {
        StopCoroutine("TypeText");
        textUI.text = "";
        currentMessage = currentLine.GetLine();
        fadeManyTransition.FadeIn();
        curShowTime = 0;
        isSpeaking = true;
        StartCoroutine("TypeText");
    }
    IEnumerator TypeText()
    {
        textUI.text = "";
        foreach (char letter in currentMessage.ToCharArray())
        {
            textUI.text += letter;
            yield return new WaitForSeconds(letterPause);
        }
    }

    [Button]
    public void LoadLines()
    {
        InkleManager.GetInstance().CreateStory();
        for (int i = 0; i < stitchesInStory.Count; i++)
        {
            var newLines = InkleManager.GetInstance().GetLinesFromSticth(stitchesInStory[i]);
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

    private void AddMonologueLines(List<MonologueLine> monologueLine, List<string> line)
    {
        for (int i = 0; i < line.Count; i++)
        {
            if (monologueLine.Find((MonologueLine x) => x.GetLine() == line[i]) == null)
            {
                monologueLine.Add(new MonologueLine(line[i], 0));
            }
        }
    }
}
