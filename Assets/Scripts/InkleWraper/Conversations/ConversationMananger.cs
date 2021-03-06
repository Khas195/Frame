using System;
using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;
using UnityEngine;


public interface IParticipant
{
	void Show(string textToShow);
	void ChooseFromChoices(List<Choice> currentChoices, Action<Choice> choiceCallBack);
	void StartConvsering();
	void StopConversing();
}
public class ConversationMananger : SingletonMonobehavior<ConversationMananger>
{
	bool conversationActive = false;
	IParticipant player;
	IParticipant other;
	Story playerConversationStory = null;
	bool playerIsChoosing = false;
	bool skipFirstLine = false;
	protected override void Awake()
	{
		base.Awake();
	}

	private void Update()
	{
		if (conversationActive && playerConversationStory != null)
		{
			LogHelper.Log("Conversation- can continue: " + playerConversationStory.canContinue);
			while (playerConversationStory.canContinue)
			{
				var sentence = playerConversationStory.Continue();
				LogHelper.Log("Conversation- Sentences: " + playerConversationStory.canContinue);
				char[] charsToTrim = { ' ', '\n' };
				sentence = sentence.Trim(charsToTrim);
				if (skipFirstLine == false)
				{
					if (other != null)
					{
						other.Show(sentence);
					}
					else
					{
						TerminateCurrentConversation();
					}
				}
				else
				{
					skipFirstLine = false;
				}
			}
			LogHelper.Log("Conversation- Current choices: " + playerConversationStory.currentChoices.Count);
			if (playerConversationStory.currentChoices.Count > 0)
			{
				if (playerIsChoosing == false)
				{
					player.ChooseFromChoices(playerConversationStory.currentChoices, (Choice chosenChoice) =>
							{
								playerConversationStory.ChooseChoiceIndex(chosenChoice.index);
								playerIsChoosing = false;
								LogHelper.Log("Conversation- player chose: " + chosenChoice.text);
								if (playerConversationStory.canContinue)
								{
									playerConversationStory.Continue();
								}
							});
					playerIsChoosing = true;
				}
			}
			else
			{
				this.TerminateCurrentConversation();
				LogHelper.Log("Conversation- Conversation terminated: ");
			}

		}
	}

	public bool HasStory()
	{
		return playerConversationStory != null;
	}

	public void TerminateCurrentConversation()
	{
		conversationActive = false;
		playerIsChoosing = false;
		player.StopConversing();
		other.StopConversing();

	}

	public void RequestPlayerConversation(string conversationStitch, IParticipant player, IParticipant other, bool skipFirstLine = false)
	{
		this.player = player;
		this.other = other;
		playerConversationStory = InkleManager.GetInstance().GetPlayerConversation();
		playerConversationStory.ChoosePathString(conversationStitch);
		conversationActive = true;
		player.StartConvsering();
		other.StartConvsering();
		this.skipFirstLine = skipFirstLine;
	}
}
