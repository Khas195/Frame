using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;
public class CharacterPublishMonologue : MonoBehaviour, IObserver
{

	[SerializeField]
	MonologueData monologueData = null;

	[SerializeField]
	[ReadOnly]
	MonologueLine lastSpokenLine = null;

	[SerializeField]
	FadeManyTransition fadeManyTransition = null;
	[SerializeField]
	Text textUI;
	[SerializeField]
	[MinMaxSlider(1, 10)]
	Vector2 restTime;
	[SerializeField]
	[ReadOnly]
	float curShowTime = 0;
	[SerializeField]
	[ReadOnly]
	float targetRestTime = 0;
	[SerializeField]
	[ReadOnly]
	float curRestTime = 100f;
	[SerializeField]
	[ReadOnly]
	MonologueLine currentLine = null;
	[SerializeField]
	float letterPause = 0.02f;
	[SerializeField]
	[ReadOnly]
	string currentMessage = "";
	bool isSpeaking = false;
	bool isResting = false;
	[SerializeField]
	List<MonologueLine> possibleLineToSpeak;
	// Start is called before the first frame update

	private void Awake()
	{
		PostOffice.Subscribes(this, GameEvent.ConversationEvent.CONVERSATION_START);
		PostOffice.Subscribes(this, GameEvent.ConversationEvent.CONVERSATION_END);
	}
	private void OnDestroy()
	{
		PostOffice.Unsubscribes(this, GameEvent.ConversationEvent.CONVERSATION_START);
		PostOffice.Unsubscribes(this, GameEvent.ConversationEvent.CONVERSATION_END);

	}

	private void Start()
	{
		if (monologueData)
		{
			monologueData.SortPiority();
		}
	}
	// Update is called once per frame
	void Update()
	{
		if (isSpeaking == false && isResting == false)
		{
			if (monologueData)
			{
				Speak();
			}
		}
		else
		{
			if (isSpeaking)
			{
				if (curShowTime <= currentLine.GetShowTime())
				{
					curShowTime += Time.deltaTime;
				}
				else
				{
					StartResting();
				}
			}
			else if (isResting)
			{
				if (curRestTime <= targetRestTime)
				{
					curRestTime += Time.deltaTime;
				}
				else
				{
					isResting = false;
				}
			}

		}
	}

	private void StartResting()
	{
		fadeManyTransition.FadeOut();
		curRestTime = 0;
		targetRestTime = UnityEngine.Random.Range(restTime.x, restTime.y);
		isSpeaking = false;
		isResting = true;
	}

	private void Speak()
	{
		var possibleLines = new List<MonologueLine>();
		possibleLines.AddRange(monologueData.monologueLines.FindAll((x) => x.IsAllConditionSatisfied() == true && x != lastSpokenLine));
		possibleLines = FindAllElementsWithHighestPiority(possibleLines);
		possibleLineToSpeak = possibleLines;
		if (possibleLines.Count > 0)
		{
			currentLine = possibleLines[UnityEngine.Random.Range(0, possibleLines.Count)];
		}
		else
		{
			currentLine = null;
		}
		lastSpokenLine = currentLine;

		ShowLine(currentLine);
	}


	private List<MonologueLine> FindAllElementsWithHighestPiority(List<MonologueLine> possibleLines)
	{
		var result = new List<MonologueLine>();
		var highestPiority = 0;

		for (int i = 0; i < possibleLines.Count; i++)
		{
			if (possibleLines[i].GetPiority() >= highestPiority)
			{
				highestPiority = possibleLines[i].GetPiority();
				result.Add(possibleLines[i]);
			}
		}

		return result;
	}

	private void ShowLine(MonologueLine currentLine)
	{
		StopCoroutine("TypeText");
		textUI.text = "";
		currentMessage = currentLine.GetLine();
		fadeManyTransition.FadeIn();
		curShowTime = 0;
		isSpeaking = true;
		StartCoroutine("TypeText");
	}
	IEnumerator TypeText()
	{
		textUI.text = "";
		foreach (char letter in currentMessage.ToCharArray())
		{
			textUI.text += letter;
			yield return new WaitForSeconds(letterPause);
		}
	}

	public void ReceiveData(DataPack pack, string eventName)
	{
		if (eventName.Equals(GameEvent.ConversationEvent.CONVERSATION_START))
		{
			fadeManyTransition.SetFadeInAlpha(0.3f);
		}
		else if (eventName.Equals(GameEvent.ConversationEvent.CONVERSATION_END))
		{
			fadeManyTransition.SetFadeInAlpha(1.0f);
		}
	}
}
