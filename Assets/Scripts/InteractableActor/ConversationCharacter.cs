using System;
using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;
using UnityEngine;
using UnityEngine.UI;

public class ConversationCharacter : MonoBehaviour, IParticipant
{
    [SerializeField]
    FadeManyTransition textBoxControl = null;
    [SerializeField]
    Text textUI = null;
    [SerializeField]
    string conversationStitch;
    [SerializeField]
    bool isInConvesation = false;
    bool playerInRange = false;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerInRange = true;
            ConversationMananger.GetInstance().RequestPlayerConversation(conversationStitch, other.gameObject.GetComponent<IParticipant>(), this);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
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
        LogHelper.Log("Conversation-" + this.gameObject.name + "stops conversing");
        if (textBoxControl.IsFadeOut() == false)
        {
            textBoxControl.FadeOut();
        }
        isInConvesation = false;
    }

    public void StartConvsering()
    {
        LogHelper.Log("Conversation-" + this.gameObject.name + "starts conversing");
        if (textBoxControl.IsFadeOut())
        {
            textBoxControl.FadeIn();
        }
        isInConvesation = true;
    }
}
