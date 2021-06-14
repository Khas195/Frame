﻿using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoadingManager : SingletonMonobehavior<SceneLoadingManager>, IObserver
{
	[SerializeField]
	BuildProfile profile = null;
	[SerializeField]
	[Scene]
	List<string> mapScenes = new List<string>();
	List<AsyncOperation> scenesLoading = new List<AsyncOperation>();
	GameInstance currentInstance = null;



	GameInstance instanceToLoad = null;



	[SerializeField]
	[ReadOnly]
	bool loadAndWait = true;
	void Start()
	{
		PostOffice.Subscribes(this, GameMasterEvent.GAME_LOAD_EVENT);
		SceneManager.sceneLoaded += OnSceneLoaded;
	}
	public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		if (scene.name.Equals(profile.loadScene))
		{
			if (currentInstance != null)
			{
				UnloadInstance(currentInstance, removeDubplicate: false);
			}
			LoadInstance(instanceToLoad, loadDuplicate: false);
		}

	}

	public void FinishedLoading()
	{
		if (loadAndWait)
		{
			for (int i = 0; i < scenesLoading.Count; i++)
			{
				LogHelper.Log("Activate scene: " + scenesLoading[i]);
				scenesLoading[i].allowSceneActivation = true;
			}
		}
		GameMaster.GetInstance().RequestGameState(this.instanceToLoad.desiredGameState);
	}
	public void FinishedLoadingProtocol()
	{
		LogHelper.Log("Finished Loading Protocol");
		SceneManager.UnloadSceneAsync(profile.loadScene);
		var data = DataPool.GetInstance().RequestInstance();
		data.SetValue("Instance", currentInstance);
		PostOffice.SendData(data, GameMasterEvent.GAME_LOAD_EVENT);
		DataPool.GetInstance().ReturnInstance(data);
	}

	public void InitiateLoadingSequenceFor(GameInstance newInstance, bool loadAndWait)
	{
		instanceToLoad = newInstance;
		GameMaster.GetInstance().RequestGameState(GameState.GameStateEnum.LoadState);
		this.loadAndWait = loadAndWait;
	}
	public GameInstance GetCurrentLoadInstance()
	{
		return instanceToLoad;
	}
	public GameInstance GetCurrentInstance()
	{
		return currentInstance;
	}
	public void LoadLoadingScene(bool loadWithTaxiScene = false)
	{
		if (loadWithTaxiScene)
		{
			profile.loadScene = profile.loadScene_Taxi;
		}
		else
		{
			profile.loadScene = profile.loadScene_Picture;
		}
		this.LoadSceneAdditively(profile.loadScene);
	}

	public float GetLoadingProgress()
	{
		var totalProgress = 0.0f;
		for (int i = 0; i < this.scenesLoading.Count; i++)
		{
			if (scenesLoading[i].allowSceneActivation == false)
			{
				totalProgress += scenesLoading[i].progress + 0.1f;
			}
			else
			{
				totalProgress += scenesLoading[i].progress;
			}
		}
		return totalProgress / this.scenesLoading.Count;
	}

	public void LoadInstance(GameInstance requestedInstance, bool loadDuplicate = true)
	{
		for (int i = 0; i < requestedInstance.sceneList.Count; i++)
		{
			if (loadDuplicate == false && currentInstance != null)
			{
				if (currentInstance.sceneList.Contains(requestedInstance.sceneList[i]) == false)
				{
					LoadSceneAdditively(requestedInstance.sceneList[i]);
				}
				else if (mapScenes.Contains(requestedInstance.sceneList[i]))
				{
					LoadSceneAdditively(requestedInstance.sceneList[i]);
				}
			}
			else
			{
				LoadSceneAdditively(requestedInstance.sceneList[i]);
			}

		}
		currentInstance = requestedInstance;

	}

	public void UnloadInstance(GameInstance instance, bool removeDubplicate = true)
	{
		for (int i = 0; i < instance.sceneList.Count; i++)
		{
			if (removeDubplicate == false)
			{
				if (instanceToLoad.sceneList.Contains(instance.sceneList[i]) == false)
				{
					SceneManager.UnloadSceneAsync(instance.sceneList[i]);
				}
				else if (mapScenes.Contains(instance.sceneList[i]))
				{
					SceneManager.UnloadSceneAsync(instance.sceneList[i]);
				}
			}
			else
			{
				SceneManager.UnloadSceneAsync(instance.sceneList[i]);
			}
		}
	}

	public void LoadSceneAdditively(string sceneName)
	{
		LogHelper.Log(" Loading Additively " + sceneName.Bolden() + "", true);
		var operation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
		if (loadAndWait && (sceneName != this.profile.loadScene && sceneName != this.profile.logScene))
		{
			LogHelper.Log(" Deactivate scene on loaded: " + sceneName.Bolden() + "", true);
			operation.allowSceneActivation = false;
		}
		this.scenesLoading.Add(operation);
	}
	public void UnloadAllScenes(string exception = "")
	{
		int numOfScene = SceneManager.sceneCount;
		LogHelper.Log("Loading Manager".Bolden().Colorize(Color.green) + " counts " + numOfScene + " at start", true);
		for (int i = 0; i < numOfScene; i++)
		{
			Scene scene = SceneManager.GetSceneAt(i);
			if (scene.name != exception)
			{
				LogHelper.Log(exception.Bolden().Colorize(Color.green) + " unloading " + scene.name, true);
				SceneManager.UnloadSceneAsync(scene.name);
			}
		}
	}

	public void ReceiveData(DataPack pack, string eventName)
	{
	}
}
