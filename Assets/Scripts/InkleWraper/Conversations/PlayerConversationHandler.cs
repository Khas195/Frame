using System;
using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;
using UnityEngine;
using UnityEngine.UI;

public class PlayerConversationHandler : MonoBehaviour, IParticipant
{
    [SerializeField]
    FadeManyTransition textBoxControl;
    [SerializeField]
    Text textUI = null;
    [SerializeField]
    FadeManyTransition choicesControl;
    [SerializeField]
    List<Text> choices = new List<Text>();
    [SerializeField]
    Action<Choice> onChoiceChosen = null;
    List<Choice> currentChoices;
    bool choosingChoices = false;
    int currentChoiceIndex = 0;
    private void Start()
    {
        textBoxControl.FadeOut();
        choicesControl.FadeOut();
    }
    // Update is called once per frame
    void Update()
    {
        if (onChoiceChosen != null)
        {
            bool chosen = false;
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                currentChoiceIndex = 1;
                chosen = true;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                currentChoiceIndex = 2;
                chosen = true;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                currentChoiceIndex = 3;
                chosen = true;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                currentChoiceIndex = 4;
                chosen = true;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                currentChoiceIndex = 5;
                chosen = true;
            }
            if (chosen)
            {
                textBoxControl.FadeIn();
                textUI.text = choices[currentChoiceIndex - 1].text;
                choicesControl.FadeOut();
                Invoke("ChooseChoice", 3);

            }
        }
    }

    public void ChooseChoice()
    {
        onChoiceChosen(currentChoices[currentChoiceIndex - 1]);
        onChoiceChosen = null;
        choosingChoices = false;
        textBoxControl.FadeOut();
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
                choices[i].text = currentChoices[i].text;
            }
        }
        this.currentChoices = currentChoices;
        choosingChoices = true;
    }

    public void StopConversing()
    {
        choicesControl.FadeOut();
        textBoxControl.FadeOut();
        for (int i = 0; i < choices.Count; i++)
        {
            choices[i].text = "";
        }
        textUI.text = "";
    }
}
