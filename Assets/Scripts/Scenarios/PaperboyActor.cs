using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

public class PaperboyActor : ScenarioActor
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
    [SerializeField]
    float letterPause = 0.02f;


    private void Awake()
    {

        genericLines.Clear();
        todayLines.Clear();
        InkleManager.GetInstance().OnNewPaperboyLinesAdded.AddListener(this.AskForTodayLines);
    }
    protected override void Start()
    {
        base.Start();
        genericLines.AddRange(InkleManager.GetInstance().GetGenericPaperLines());
        YellDialogue();
    }

    private void AskForTodayLines()
    {
        todayLines.Clear();
        todayLines.AddRange(InkleManager.GetInstance().RequestTodayLines());
    }
    private void FixedUpdate()
    {
        if (todayLines.Count == 0)
        {
            AskForTodayLines();
        }
    }
    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
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
            this.myCanvas.gameObject.SetActive(true);
            var chosenLine = "";
            do
            {
                chosenLine = listToRead[Random.Range(0, listToRead.Count)];

            } while (listToIgnore.Contains(chosenLine) == true);

            listToIgnore.Add(chosenLine);

            currentLine = chosenLine;
        }
        else
        {
            this.myCanvas.gameObject.SetActive(false);
        }

        StopCoroutine("TypeText");
        StartCoroutine("TypeText");

        curShowTime = 0;

        genericLine = !genericLine;
        if (listToIgnore.Count == listToRead.Count)
        {
            listToIgnore.Clear();
        }
    }
    IEnumerator TypeText()
    {
        dialogueBox.text = "";
        foreach (char letter in currentLine.ToCharArray())
        {
            dialogueBox.text += letter;
            yield return new WaitForSeconds(letterPause);
        }
    }


}
