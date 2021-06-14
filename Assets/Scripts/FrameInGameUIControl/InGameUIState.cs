using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public abstract class InGameUIState : State
{
	public enum InGameUIStateEnum
	{
		NewsPanelState,
		NormalState,
		PhotoInventoryState,
		CapturingState,
		PhotoMode,
		PublishedPaperPanel,
		MapState,
		DiaryPanelState,
		OptionState
	}
	[SerializeField]
	[ReadOnly]
	protected InGameUIControl controller;
	protected void Awake()
	{
		controller = InGameUIControl.GetInstance();
	}
	public abstract void UpdateState();
}
