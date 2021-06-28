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
	[SerializeField]
	AudioSource choiceMadeSound = null;
	List<Choice> currentChoices;
	bool choosingChoices = false;

	private void Start()
	{
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
				if (chosenIndex < currentChoices.Count)
				{
					choicesControl.FadeOut();
					ChooseChoice(currentChoices[chosenIndex]);
				}
			}
		}
	}

	public void ChooseChoice(Choice chosenChoice)
	{
		onChoiceChosen(chosenChoice);
		onChoiceChosen = null;
		choosingChoices = false;
		choiceMadeSound.Play();
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
	private void OnTriggerStay2D(Collider2D other)
	{
		if (other.attachedRigidbody == null) return;
		var conversationChar = other.attachedRigidbody.gameObject.GetComponentInChildren<ConversationCharacter>();
		if (conversationChar)
		{
			if (conversationChar.IsInConversation() == false && onChoiceChosen == null)
			{
				var manager = ConversationMananger.GetInstance(forceCreate: false);
				if (manager && manager.HasStory())
				{
					var tempChoice = new Ink.Runtime.Choice();
					List<Ink.Runtime.Choice> fakeList = new List<Ink.Runtime.Choice>();
					fakeList.Add(tempChoice);
					tempChoice.text = "Hey.";
					this.ChooseFromChoices(fakeList, (Ink.Runtime.Choice choice) =>
					{
						conversationChar.RequestStartConversation();
					});
				}
			}
		}
	}
	public void StopConversing()
	{
		Debug.Log("Conversation- Player stops conversing");
		if (choicesControl.IsFadeOut() == false)
		{
			choicesControl.FadeOut();
		}
		PostOffice.SendData(null, GameEvent.ConversationEvent.CONVERSATION_END);
	}

	public void StartConvsering()
	{
		Debug.Log("Conversation- Player starts conversing");
		if (choicesControl.IsFadeOut() == true)
		{
			choicesControl.FadeIn();
		}
		PostOffice.SendData(null, GameEvent.ConversationEvent.CONVERSATION_START);
	}
}
