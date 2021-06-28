using System;
using System.Collections;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DaySystem : SingletonMonobehavior<DaySystem>, IObserver
{
	[SerializeField]
	UnityEvent OnDayChangedEvent;
	[SerializeField]
	[Expandable]
	DaySystemData dayData;
	[SerializeField]
	[Expandable]
	PublicSwayData swayData;
	[SerializeField]
	PublishedPapersData publishedPaperData;
	[SerializeField]
	DayCheckCondition dayConditionForEnding = null;
	[SerializeField]
	string warEndScenario = "";
	[SerializeField]
	GameInstance endingWar = null;
	[SerializeField]
	string redEndScenario = "";
	[SerializeField]
	GameInstance endingRed = null;
	[SerializeField]
	string blueEndScenario = "";
	[SerializeField]
	GameInstance endingBlue = null;
	[SerializeField]
	GameInstance ending = null;

	protected override void Awake()
	{
		PostOffice.Subscribes(this, GameEvent.InGameUiStateEvent.ON_IN_GAME_UIS_STATE_CHANGED);
		dayData.ResetDay();
		Scenario.OnScenarioEnter.AddListener(OnNewScenarioEnter);
	}
	private void OnDestroy()
	{
		PostOffice.Unsubscribes(this, GameEvent.InGameUiStateEvent.ON_IN_GAME_UIS_STATE_CHANGED);
	}
	public void OnNewScenarioEnter(Scenario newScenario)
	{
		if (newScenario.GetScenarioName() == warEndScenario)
		{
			ending = endingWar;
		}
		else if (newScenario.GetScenarioName() == blueEndScenario)
		{
			ending = endingBlue;
		}
		else if (newScenario.GetScenarioName() == redEndScenario)
		{
			ending = endingRed;
		}

	}
	public int GetCurrentDay()
	{
		return dayData.currentDay;
	}
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.L))
		{
			NextDay();
		}
	}
	[Button]
	public void NextDay()
	{
		OnDayChangedEvent.Invoke();
		SetCurrentDay(dayData.currentDay + 1);
		InGameUIControl.GetInstance().RequestState(InGameUIState.InGameUIStateEnum.NormalState);
		if (dayConditionForEnding.IsSatisfied())
		{
			GameMaster.GetInstance().RequestInstance(this.ending, true);
		}
		else
		{
			GameMaster.GetInstance().RequestInstance(SceneLoadingManager.GetInstance().GetCurrentInstance(), true);
		}
	}

	private bool HasPublishedEnoughPaper()
	{
		return dayData.currentDay < dayData.amountOfPaperNeededPerDay.Count;
	}

	public void SetCurrentDay(int newDay)
	{
		dayData.currentDay = newDay;
		TriggerOnDayChangedEvent();
	}

	private void TriggerOnDayChangedEvent()
	{
		var data = DataPool.GetInstance().RequestInstance();
		data.SetValue(GameEvent.DaySystemEvent.OnDayChangedEventData.CURRENT_DAY, dayData.currentDay);
		PostOffice.SendData(data, GameEvent.DaySystemEvent.DAY_CHANGED_EVENT);
		DataPool.GetInstance().ReturnInstance(data);
	}

	public void ReceiveData(DataPack pack, string eventName)
	{
		if (eventName == GameEvent.InGameUiStateEvent.ON_IN_GAME_UIS_STATE_CHANGED)
		{
			var newState = pack.GetValue<InGameUIState.InGameUIStateEnum>(GameEvent.InGameUiStateEvent.OnInGameUIsStateChangedData.NEW_STATE);
			if (newState == InGameUIState.InGameUIStateEnum.NormalState)
			{
				if (CanProceedToNextDay())
				{
					NextDay();
				}
			}
		}
	}

	private bool CanProceedToNextDay()
	{
		return HasPublishedEnoughPaper() && publishedPaperData.paperDatas.Count >= dayData.amountOfPaperNeededPerDay[dayData.currentDay];
	}
}