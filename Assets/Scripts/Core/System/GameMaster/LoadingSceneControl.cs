using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingSceneControl : MonoBehaviour
{
    [SerializeField]
    Text loadingProgress = null;
    [SerializeField]
    Text currentLoading = null;
    // Update is called once per frame
    void Update()
    {
        loadingProgress.text = SceneLoadingManager.GetInstance().GetLoadingProgress().ToString();


    }

    public void SetLoadedText(string name)
    {
        currentLoading.text = name + " loaded!!";
    }
}
