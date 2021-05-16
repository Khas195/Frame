using System;
using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;
using UnityEngine;
using UnityEngine.UI;

public class PlayerConversationHandler : MonoBehaviour, IParticipant
{
    [SerializeField]
    FadeManyTransition choicesControl;
    [SerializeField]
    List<ChoiceUI> choices = new List<ChoiceUI>();
    [SerializeField]
    Action<Choice> onChoiceChosen = null;
    List<Choice> currentChoices;
    bool choosingChoices = false;
    private void Start()
    {
        choicesControl.FadeOut();
    }
    // Update is called once per frame
    void Update()
    {
        if (onChoiceChosen != null)
        {
            int chosenIndex = -1;
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                chosenIndex = 0;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                chosenIndex = 1;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                chosenIndex = 2;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                chosenIndex = 3;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                chosenIndex = 4;
            }
            if (chosenIndex >= 0)
            {
                choicesControl.FadeOut();
                ChooseChoice(currentChoices[chosenIndex]);

            }
        }
    }

    public void ChooseChoice(Choice chosenChoice)
    {
        onChoiceChosen(chosenChoice);
        onChoiceChosen = null;
        choosingChoices = false;
    }

    public void Show(string textToShow)
    {
    }
    public void ChooseFromChoices(List<Choice> currentChoices, Action<Choice> choiceCallBack)
    {
        onChoiceChosen = choiceCallBack;
        choicesControl.FadeIn();
        for (int i = 0; i < choices.Count; i++)
        {
            if (i >= currentChoices.Count)
            {
                choices[i].gameObject.SetActive(false);
            }
            else
            {
                choices[i].gameObject.SetActive(true);
                choices[i].SetText(currentChoices[i].text);
            }
        }
        this.currentChoices = currentChoices;
        choosingChoices = true;
    }

    public void StopConversing()
    {
        LogHelper.Log("Conversation- Player stops conversing");
        if (choicesControl.IsFadeOut() == false)
        {
            choicesControl.FadeOut();
        }
    }

    public void StartConvsering()
    {
    }
}
