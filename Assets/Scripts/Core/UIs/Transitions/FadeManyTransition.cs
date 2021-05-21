using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class FadeManyTransition : MonoBehaviour
{
    [SerializeField]
    float fadeTime = 3.0f;
    [SerializeField]
    List<Graphic> uis;

    [SerializeField]
    [ReadOnly]
    float currentTime = 0.0f;
    [SerializeField]
    [ReadOnly]
    float targetAlpha = 0.0f;
    [SerializeField]
    [ReadOnly]
    float startAlpha = 1.0f;

    [SerializeField]
    [ReadOnly]
    bool isTransitioning = false;
    [SerializeField]
    [ReadOnly]
    bool isFadedOut = false;
    [SerializeField]
    Action callback = null;
    [SerializeField]
    bool startFadeOut = true;


    private void Start()
    {
        FindAllCurrentUis();
        if (startFadeOut)
        {
            SetAllUisAlpha(0);
            isFadedOut = true;
        }
        else
        {
            SetAllUisAlpha(1);
            isFadedOut = false;
        }
    }

    private void FindAllCurrentUis()
    {
        this.uis.Clear();
        this.uis.AddRange(this.GetComponentsInChildren<Graphic>(true));
    }

    void Update()
    {
        if (isTransitioning)
        {
            for (int i = 0; i < uis.Count; i++)
            {
                if (uis[i] != null)
                {
                    var color = uis[i].color;
                    color.a = Mathf.Lerp(startAlpha, targetAlpha, currentTime / fadeTime);
                    uis[i].color = color;
                }
            }
            if (currentTime >= fadeTime)
            {
                isTransitioning = false;
                if (callback != null)
                {
                    callback();
                    callback = null;
                }
                if (targetAlpha <= 0.1f)
                {
                    isFadedOut = true;
                }
                else
                {
                    isFadedOut = false;
                }
            }
            currentTime += Time.deltaTime;
        }
    }
    public void SetFadeTime(float newFadeTime)
    {
        this.fadeTime = newFadeTime;
    }

    private void SetAllUisAlpha(float newAlpha)
    {
        for (int i = 0; i < uis.Count; i++)
        {
            var color = uis[i].color;
            color.a = newAlpha;
            uis[i].color = color;
        }
    }
    public bool IsFadeOut()
    {
        return isFadedOut;
    }

    [Button]
    public void FadeIn(Action callback = null)
    {
        FindAllCurrentUis();
        startAlpha = 0.0f;
        SetAllUisAlpha(startAlpha);
        targetAlpha = 1.0f;
        currentTime = 0;
        isTransitioning = true;
        this.callback = callback;
    }
    [Button]
    public void FadeOut(Action callback = null)
    {
        FindAllCurrentUis();
        startAlpha = 1.0f;
        SetAllUisAlpha(startAlpha);
        targetAlpha = 0.0f;
        currentTime = 0;
        isTransitioning = true;
        this.callback = callback;
    }

}
