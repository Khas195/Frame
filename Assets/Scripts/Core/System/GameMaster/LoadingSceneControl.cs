using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingSceneControl : SingletonMonobehavior<LoadingSceneControl>
{
    [SerializeField]
    Text loadingProgress = null;
    [SerializeField]
    Text currentLoading = null;
    [SerializeField]
    Image loadingUnfillImage = null;
    [SerializeField]
    float minLoadTime = 1f;
    [SerializeField]
    [ReadOnly]
    float curTime = 0.0f;
    private void Start()
    {
        currentLoading.text = "Loading...";
        var unfillColor = loadingUnfillImage.color;
        unfillColor.a = 1.0f;
        loadingUnfillImage.color = unfillColor;
    }
    // Update is called once per frame
    void Update()
    {
        if (curTime < minLoadTime)
        {
            var unfillColor = loadingUnfillImage.color;
            unfillColor.a = 1f - curTime / minLoadTime;
            loadingProgress.text = ((curTime / minLoadTime) * 100).ToString("F1") + "%";
            loadingUnfillImage.color = unfillColor;
            curTime += Time.deltaTime;
        }
        else
        {
            loadingProgress.text = (SceneLoadingManager.GetInstance().GetLoadingProgress() * 100).ToString() + "%";
            if (SceneLoadingManager.GetInstance().GetLoadingProgress() >= 1.0f)
            {
                SetLoadedText("Game");
                SceneLoadingManager.GetInstance().FinishedLoading();
            }
        }
    }

    public void SetLoadedText(string name)
    {
        currentLoading.text = name + " loaded!!";
    }
}
