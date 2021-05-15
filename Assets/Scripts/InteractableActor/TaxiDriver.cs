using System;
using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;
using UnityEngine;
using UnityEngine.UI;

public class TaxiDriver : MonoBehaviour, IParticipant
{
    [SerializeField]
    FadeManyTransition textBoxControl = null;
    [SerializeField]
    Text textUI = null;
    [SerializeField]
    string conversationStitch;
    bool playerInRange = false;
    private void Start()
    {
        textBoxControl.FadeOut();
        var story = InkleManager.GetInstance().GetPlayerConversation();
        story.BindExternalFunction("OpenTaxiMap", () =>
        {
            InGameUIControl.GetInstance().RequestState(InGameUIState.InGameUIStateEnum.MapState);
        });
    }
    private void Update()
    {
        if (playerInRange)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                InGameUIControl.GetInstance().RequestState(InGameUIState.InGameUIStateEnum.MapState);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            textBoxControl.FadeIn();
            playerInRange = true;
            ConversationMananger.GetInstance().RequestPlayerConversation(conversationStitch, other.gameObject.GetComponent<IParticipant>(), this);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            textBoxControl.FadeOut();
            playerInRange = false;
            ConversationMananger.GetInstance().TerminateCurrentConversation();
        }
    }

    public void Show(string textToShow)
    {
        textUI.text = textToShow;
    }

    public void ChooseFromChoices(List<Ink.Runtime.Choice> currentChoices, Action<Choice> choiceCallBack)
    {
    }

    public void StopConversing()
    {
        textBoxControl.FadeOut();
        textUI.text = "";
    }
}
