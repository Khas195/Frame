using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoadingManager : SingletonMonobehavior<SceneLoadingManager>, IObserver
{
    [SerializeField]
    BuildProfile profile = null;
    List<AsyncOperation> scenesLoading = new List<AsyncOperation>();
    GameInstance currentInstance = null;
    GameInstance instanceToLoad = null;
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
                UnloadInstance(currentInstance);
            }
            LoadInstance(instanceToLoad);
        }

    }

    public void FinishedLoading()
    {
        GameMaster.GetInstance().RequestGameState(this.instanceToLoad.desiredGameState);

    }
    public void FinishedLoadingProtocol()
    {
        SceneManager.UnloadSceneAsync(profile.loadScene);
        var data = DataPool.GetInstance().RequestInstance();
        data.SetValue("Instance", currentInstance);
        PostOffice.SendData(data, GameMasterEvent.GAME_LOAD_EVENT);
        DataPool.GetInstance().ReturnInstance(data);
    }

    public void InitiateLoadingSequenceFor(GameInstance newInstance)
    {
        instanceToLoad = newInstance;
        GameMaster.GetInstance().RequestGameState(GameState.GameStateEnum.LoadState);
    }

    public void LoadLoadingScene()
    {
        this.LoadSceneAdditively(profile.loadScene);
    }

    public float GetLoadingProgress()
    {
        var totalProgress = 0.0f;
        for (int i = 0; i < this.scenesLoading.Count; i++)
        {
            totalProgress += scenesLoading[i].progress;
        }
        return totalProgress / this.scenesLoading.Count;
    }

    public void LoadInstance(GameInstance requestedInstance)
    {
        currentInstance = requestedInstance;
        for (int i = 0; i < requestedInstance.sceneList.Count; i++)
        {
            LoadSceneAdditively(requestedInstance.sceneList[i]);
        }
    }

    public void UnloadInstance(GameInstance instance)
    {
        for (int i = 0; i < instance.sceneList.Count; i++)
        {
            SceneManager.UnloadSceneAsync(instance.sceneList[i]);
        }
    }
    public void LoadSceneAdditively(string sceneName)
    {
        LogHelper.Log(" Loading Additively " + sceneName.Bolden() + "", true);
        var operation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
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
