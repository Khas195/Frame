using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

public class PaperboyActor : MonoBehaviour
{
    [SerializeField]
    Canvas myCanvas;
    [SerializeField]
    Text dialogueBox;
    [SerializeField]
    List<string> todayLines = new List<string>();

    [SerializeField]
    [ReadOnly]
    List<string> readTodayLines = new List<string>();
    [SerializeField]
    List<string> genericLines = new List<string>();
    [SerializeField]
    [ReadOnly]
    List<string> readGenericLines = new List<string>();

    [SerializeField]
    float showTime = 3.0f;

    [SerializeField]
    [ReadOnly]
    float curShowTime = 0.0f;
    [SerializeField]
    [ReadOnly]
    bool genericLine = true;
    [ReadOnly]
    string currentLine = "";



    private void Awake()
    {
        genericLines.Clear();
        todayLines.Clear();
        Scenario.OnScenarioEnter.AddListener(this.AskForTodayLines);
    }
    void Start()
    {
        genericLines.AddRange(InkleManager.GetInstance().GetGenericPaperLines());
        YellDialogue();
    }

    private void AskForTodayLines(Scenario newScenario)
    {
        todayLines.AddRange(InkleManager.GetInstance().RequestTodayLines());
    }

    // Update is called once per frame
    void Update()
    {
        if (curShowTime <= showTime)
        {
            curShowTime += Time.deltaTime;
        }
        if (curShowTime > showTime)
        {
            YellDialogue();
        }
    }

    private void YellDialogue()
    {
        var listToRead = genericLines;
        var listToIgnore = readGenericLines;
        if (genericLine == false)
        {
            listToRead = todayLines;
            listToIgnore = readTodayLines;
        }
        if (listToRead.Count > 0)
        {
            var chosenLine = "";
            do
            {
                chosenLine = listToRead[Random.Range(0, listToRead.Count)];

            } while (listToIgnore.Contains(chosenLine) == true);

            listToIgnore.Add(chosenLine);

            currentLine = chosenLine;
        }

        dialogueBox.text = currentLine;

        curShowTime = 0;

        genericLine = !genericLine;
        if (listToIgnore.Count == listToRead.Count)
        {
            listToIgnore.Clear();
        }
    }
}
