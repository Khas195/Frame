using UnityEngine;
public class StatueHonoredEndingSounds : MonoBehaviour
{

	[SerializeField]
	GameInstance mainMenuInstance = null;
	[SerializeField]
	AudioSource flagFlaping;
	[SerializeField]
	AudioSource peopleShouting;
	[SerializeField]
	AudioSource carDrivingIn01;
	[SerializeField]
	AudioSource carDrivingIn02;
	[SerializeField]
	AudioSource carOpen;
	[SerializeField]
	AudioSource carClose;




	public void BackToMainMenu()
	{
		GameMaster.GetInstance().RequestInstance(mainMenuInstance);
	}
	public void PlayFlagFlapping()
	{
		flagFlaping.Play();
	}
	public void StopFlagFlapping()
	{
		flagFlaping.Stop();
	}
	public void PlayPeopleShouting()
	{
		peopleShouting.Play();
	}
	public void PlayCarOpen()
	{
		carOpen.Play();
	}
	public void PlayCarClose()
	{
		carClose.Play();
	}
	public void PlayCarDrivingIn01()
	{
		carDrivingIn01.Play();
	}
	public void PlayCarDrivingIn02()
	{
		carDrivingIn02.Play();
	}
}
