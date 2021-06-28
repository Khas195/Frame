using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class StatueBrokenEndingSounds : MonoBehaviour
{
	[SerializeField]
	AudioSource eraser01;
	[SerializeField]
	AudioSource flagFlaping;
	[SerializeField]
	GameInstance mainMenuInstance = null;

	public void PlayErasor01()
	{
		eraser01.Play();
	}

	public void PlayFlagFlapping()
	{
		flagFlaping.Play();
	}
	public void BackToMainMenu()
	{
		GameMaster.GetInstance().RequestInstance(mainMenuInstance);
	}
}
