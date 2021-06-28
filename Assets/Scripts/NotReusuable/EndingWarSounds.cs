using UnityEngine;

public class EndingWarSounds : MonoBehaviour
{
	[SerializeField]
	AudioSource flagShred;
	[SerializeField]
	AudioSource protestorsStart;
	[SerializeField]
	AudioSource fireBurning;
	[SerializeField]
	AudioSource swing;
	[SerializeField]
	AudioSource punchHit;
	[SerializeField]
	GameInstance mainMenuInstance = null;
	public void BackToMainMenu()
	{
		GameMaster.GetInstance().RequestInstance(mainMenuInstance);
	}

	public void PlayFlagShred()
	{
		flagShred.Play();
	}
	public void PlayProtestors()
	{
		protestorsStart.Play();
	}
	public void StopProtestors()
	{
		protestorsStart.Stop();
	}
	public void PlayFireBurning()
	{
		fireBurning.Play();
	}
	public void StopFireBurning()
	{
		fireBurning.Stop();
	}
	public void PlaySwing()
	{
		swing.Play();
	}
	public void PlayPunch()
	{
		punchHit.Play();
	}
}

