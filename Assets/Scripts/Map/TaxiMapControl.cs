using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaxiMapControl : MonoBehaviour, IObserver
{
	[SerializeField]
	GameObject mapPanel = null;
	[SerializeField]
	AudioSource carDoorOpen = null;
	[SerializeField]
	AudioSource carDoorClose = null;
	[SerializeField]
	Image playerIcon = null;
	[SerializeField]
	Transform picisLocation = null;
	[SerializeField]
	Transform parkLocation = null;
	[SerializeField]
	GameObject mapNormal = null;
	[SerializeField]
	GameObject mapBroken = null;
	[SerializeField]
	GameObject parkButtonNormal = null;
	[SerializeField]
	GameObject parkBrokenButton = null;
	[SerializeField]
	Button picisStreetButton = null;
	[SerializeField]
	Button parkButton = null;
	[SerializeField]
	Button parkBrokenButtonUI = null;
	[SerializeField]
	List<string> brokenScenarioNames;

	private void Start()
	{
		HideMap();
		PostOffice.Subscribes(this, GameEvent.MapChangedEvent.MAP_CHANGED_EVENT);
		PostOffice.Subscribes(this, GameEvent.DaySystemEvent.DAY_CHANGED_EVENT);
		playerIcon.transform.position = picisLocation.position;
		mapNormal.gameObject.SetActive(true);
		mapBroken.gameObject.SetActive(false);
		parkButtonNormal.gameObject.SetActive(true);
		parkBrokenButton.gameObject.SetActive(false);
	}
	private void OnDestroy()
	{
		PostOffice.Unsubscribes(this, GameEvent.MapChangedEvent.MAP_CHANGED_EVENT);
		PostOffice.Unsubscribes(this, GameEvent.DaySystemEvent.DAY_CHANGED_EVENT);

	}
	public void ShowMap()
	{
		mapPanel.SetActive(true);
		ConversationMananger.GetInstance().TerminateCurrentConversation();
	}
	public void HideMap()
	{
		mapPanel.SetActive(false);
	}
	public void ChangeMapToBroken()
	{
		LogHelper.Log("Map Control - Map broken");
		this.mapNormal.gameObject.SetActive(false);
		this.mapBroken.gameObject.SetActive(true);
		parkButtonNormal.gameObject.SetActive(false);
		parkBrokenButton.gameObject.SetActive(true);
	}
	public void LoadInstance(GameInstance instance)
	{
		var gameMaster = GameMaster.GetInstance();
		if (gameMaster.GetCurrentGameInstance() == instance) return;

		gameMaster.RequestInstance(instance);
		InGameUIControl.GetInstance().RequestState(InGameUIState.InGameUIStateEnum.NormalState);
		carDoorOpen.Play();
		carDoorClose.PlayDelayed(carDoorOpen.clip.length);
	}

	public void ReceiveData(DataPack pack, string eventName)
	{
		if (eventName.Equals(GameEvent.MapChangedEvent.MAP_CHANGED_EVENT))
		{
			var currentLocaton = pack.GetValue<GameEvent.MapChangedEvent.MapLocation>(GameEvent.MapChangedEvent.MAP_LOCATION_DATA);
			if (currentLocaton == GameEvent.MapChangedEvent.MapLocation.PicisStreet)
			{
				picisStreetButton.interactable = false;
				parkBrokenButtonUI.interactable = true;
				parkButton.interactable = true;
				playerIcon.transform.position = picisLocation.position;
			}
			else
			{
				picisStreetButton.interactable = true;
				parkBrokenButtonUI.interactable = false;
				parkButton.interactable = false;
				playerIcon.transform.position = parkLocation.position;
			}
		}
		if (eventName.Equals(GameEvent.DaySystemEvent.DAY_CHANGED_EVENT))
		{
			bool condition = true;
			var currentScenario = ScenarioManager.GetInstance().GetActiveScenario();
			if (brokenScenarioNames.Contains(currentScenario.GetScenarioName()))
			{
				condition = true;
			}
			else
			{
				condition = false;
			}

			if (condition == true)
			{
				ChangeMapToBroken();
			}
		}
	}
}
