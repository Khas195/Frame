using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Audio;

public class OptionPanelControl : MonoBehaviour
{
	[SerializeField]
	[Required]
	InGameUIControl uIControl;
	[SerializeField]
	[Required]
	GameInstance mainmenuInstance;
	[SerializeField]
	AudioMixer audioMixer;
	[SerializeField]
	public void QuitGame()
	{
		uIControl.RequestState(InGameUIState.InGameUIStateEnum.NormalState);
		Invoke("CallGameMasterExit", 0.5f);
	}
	public void CallGameMasterExit()
	{
		GameMaster.GetInstance().ExitGame();
	}
	public void GoToMainMenu()
	{
		uIControl.RequestState(InGameUIState.InGameUIStateEnum.NormalState);
		Invoke("RequestMainmenuInstanceFromGameMaster", 0.5f);
	}
	public void RequestMainmenuInstanceFromGameMaster()
	{
		GameMaster.GetInstance().RequestInstance(mainmenuInstance);
	}
	public void QuitOption()
	{
		uIControl.RequestState(InGameUIState.InGameUIStateEnum.NormalState);
	}
	public void OnMusicValueChanged(float amount)
	{
		audioMixer.SetFloat("MusicVolumn", Mathf.Lerp(-20, 20, amount));
	}
	public void OnSoundValueChanged(float amount)
	{
		audioMixer.SetFloat("SoundVolumn", Mathf.Lerp(-20, 20, amount));
	}
}
