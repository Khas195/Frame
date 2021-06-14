using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventOnDay : MonoBehaviour, IObserver
{
	[SerializeField]
	DaySystemData daySystemData;
	[SerializeField]
	int dayToShow = 0;
	[SerializeField]
	Transform eventRoots;

	private void Awake()
	{
		PostOffice.Subscribes(this, GameEvent.DaySystemEvent.DAY_CHANGED_EVENT);
	}
	private void Start()
	{
		if (daySystemData.currentDay == (dayToShow - 1))
		{
			TurnOnProps();
		}
		else
		{
			TurnOffProps();
		}
	}
	private void OnDestroy()
	{
		PostOffice.Unsubscribes(this, GameEvent.DaySystemEvent.DAY_CHANGED_EVENT);
	}
	public void ReceiveData(DataPack pack, string eventName)
	{
		if (eventName == GameEvent.DaySystemEvent.DAY_CHANGED_EVENT)
		{
			if (daySystemData.currentDay == (dayToShow - 1))
			{
				TurnOnProps();
			}
			else
			{
				TurnOffProps();
			}
		}
	}

	private void TurnOffProps()
	{
		this.eventRoots.gameObject.SetActive(false);
	}

	private void TurnOnProps()
	{
		this.eventRoots.gameObject.SetActive(true);
	}
}
