using System;
using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;
using NaughtyAttributes;
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
    IParticipant playerChar = null;

    [SerializeField]
    float wordPerSeconds = 0.02f;
    [SerializeField]
    [ReadOnly]
    string textToShow = "";

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerChar = other.gameObject.GetComponent<IParticipant>();
            playerInRange = true;
            ConversationMananger.GetInstance().RequestPlayerConversation(conversationStitch, playerChar, this);
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
    IEnumerator TypeText()
    {
        foreach (char letter in textToShow.ToCharArray())
        {
            textUI.text += letter;
            yield return new WaitForSeconds(wordPerSeconds);
        }
    }

    public void Show(string textToShow)
    {
        textUI.text = "";
        this.textToShow = textToShow;
        StartCoroutine(TypeText());
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

    public bool IsInConversation()
    {
        return isInConvesation;
    }

    public void RequestStartConversation()
    {
        if (playerChar != null && playerInRange)
        {
            ConversationMananger.GetInstance().RequestPlayerConversation(conversationStitch, playerChar, this);
        }
    }
}
