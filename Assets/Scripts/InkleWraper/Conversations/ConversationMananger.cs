using System;
using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;
using UnityEngine;


public interface IParticipant
{
    void Show(string textToShow);
    void ChooseFromChoices(List<Choice> currentChoices, Action<Choice> choiceCallBack);
    void StopConversing();
}
public class ConversationMananger : SingletonMonobehavior<ConversationMananger>
{
    bool conversationActive = false;
    IParticipant player;
    IParticipant other;
    Story playerConversationStory = null;
    bool playerIsChoosing = false;
    protected override void Awake()
    {
        base.Awake();
    }
    private void Start()
    {
        playerConversationStory = InkleManager.GetInstance().GetPlayerConversation();
    }
    private void Update()
    {
        if (conversationActive)
        {
            while (playerConversationStory.canContinue)
            {
                var sentence = playerConversationStory.Continue();
                sentence.Trim();
                other.Show(sentence);
            }
            if (playerConversationStory.currentChoices.Count > 0)
            {
                if (playerIsChoosing == false)
                {
                    player.ChooseFromChoices(playerConversationStory.currentChoices, (Choice chosenChoice) =>
                                    {
                                        playerConversationStory.ChooseChoiceIndex(chosenChoice.index);
                                        playerIsChoosing = false;
                                    });
                    playerIsChoosing = true;
                }
            }
            else
            {
                this.TerminateCurrentConversation();
            }

        }
    }

    public void TerminateCurrentConversation()
    {
        conversationActive = false;
        player.StopConversing();
        other.StopConversing();
    }

    public void RequestPlayerConversation(string conversationStitch, IParticipant player, IParticipant other)
    {
        this.player = player;
        this.other = other;
        playerConversationStory.ChoosePathString(conversationStitch);
        conversationActive = true;
    }
}
