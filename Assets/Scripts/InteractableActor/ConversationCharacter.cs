using System;
using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

public class ConversationCharacter : MonoBehaviour, IParticipant, IObserver
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
	AudioSource textPrintSound = null;
	[SerializeField]
	float letterPause = 0.02f;
	[SerializeField]
	[ReadOnly]
	string textToShow = "";
	bool reactToPaper = false;
	private void Start()
	{
		PostOffice.Subscribes(this, GameEvent.NewspaperEvent.NEWSPAPER_PUBLISHED_EVENT);
	}
	private void OnDestroy()
	{
		PostOffice.Unsubscribes(this, GameEvent.NewspaperEvent.NEWSPAPER_PUBLISHED_EVENT);
		StopCoroutine("TypeText");
	}
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player")
		{
			playerChar = other.gameObject.GetComponent<IParticipant>();
			playerInRange = true;
			ConversationMananger.GetInstance().RequestPlayerConversation(conversationStitch, playerChar, this);
		}
	}
	private void OnTriggerStay2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player")
		{
			LogHelper.Log("Conversation Character - player in range");
			if (InkleManager.GetInstance().GetVariable<bool>("JustPublishedPaper") == true && reactToPaper)
			{
				LogHelper.Log("Conversation Character - Newspaper just published - reacting");
				playerChar = other.gameObject.GetComponent<IParticipant>();
				ConversationMananger.GetInstance().RequestPlayerConversation(conversationStitch, playerChar, this);
				reactToPaper = false;
			}
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
		textUI.text = "";
		foreach (char letter in textToShow.ToCharArray())
		{
			textUI.text += letter;
			if (textPrintSound != null)
			{
				textPrintSound.Play();
				yield return 0;
			}
			yield return new WaitForSeconds(letterPause);
		}
	}

	public void Show(string textToShow)
	{
		StopCoroutine("TypeText");
		textUI.text = "";
		this.textToShow = textToShow;
		StartCoroutine("TypeText");
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


	public void ReceiveData(DataPack pack, string eventName)
	{
		if (eventName == GameEvent.NewspaperEvent.NEWSPAPER_PUBLISHED_EVENT)
		{
			InkleManager.GetInstance().SetVariable("JustPublishedPaper", true);
			reactToPaper = true;
		}
	}
}
