using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

public class SwayStatsPanel : MonoBehaviour, IObserver
{
    [SerializeField]
    PublicSwayData currentSwayData;
    [SerializeField]
    PublicSwayData yesterdayData;
    [SerializeField]
    Image commieSlider;
    [SerializeField]
    Image capitalistSlider;
    [SerializeField]
    Image sliderBG;
    [SerializeField]
    float transitionTime = 3.0f;
    [SerializeField]
    FadeManyTransition fadeTransition = null;

    [SerializeField]
    [ReadOnly]
    float currentTime = 0.0f;
    [SerializeField]
    [ReadOnly]
    bool isTransitioning = false;
    [SerializeField]
    [ReadOnly]
    bool newspaperPublished = false;
    [SerializeField]
    [ReadOnly]
    Action onUpdateFinishedCallback;
    private float startLeft;
    private float startRight;
    private float endLeft;
    private float endRight;

    private void Awake()
    {
        yesterdayData.swayPercentage = new Vector2(0, 100);
        PostOffice.Subscribes(this, GameEvent.InGameUiStateEvent.ON_IN_GAME_UIS_STATE_CHANGED);
        PostOffice.Subscribes(this, GameEvent.NewspaperEvent.NEWSPAPER_PUBLISHED_EVENT);
    }
    private void OnDestroy()
    {
        PostOffice.Unsubscribes(this, GameEvent.InGameUiStateEvent.ON_IN_GAME_UIS_STATE_CHANGED);
        PostOffice.Unsubscribes(this, GameEvent.NewspaperEvent.NEWSPAPER_PUBLISHED_EVENT);
    }
    [Button]
    public void TriggerSliderUpdate(Action callBack = null)
    {
        startLeft = yesterdayData.swayPercentage.x / 100;
        startRight = (100 - yesterdayData.swayPercentage.y) / 100;
        endLeft = (currentSwayData.swayPercentage.x / 100);
        endRight = ((100 - currentSwayData.swayPercentage.y) / 100);
        yesterdayData.swayPercentage = currentSwayData.swayPercentage;

        commieSlider.fillAmount = startLeft;
        capitalistSlider.fillAmount = startRight;
        isTransitioning = true;
        currentTime = 0;
        this.onUpdateFinishedCallback = callBack;
    }
    private void Update()
    {
        if (isTransitioning)
        {
            if (currentTime <= transitionTime)
            {
                commieSlider.fillAmount = Mathf.Lerp(startLeft, endLeft, currentTime / transitionTime);
                capitalistSlider.fillAmount = Mathf.Lerp(startRight, endRight, currentTime / transitionTime);

                currentTime += Time.deltaTime;
            }
            else
            {
                isTransitioning = false;
                if (onUpdateFinishedCallback != null)
                {
                    onUpdateFinishedCallback.Invoke();
                    onUpdateFinishedCallback = null;
                }

            }
        }

    }
    public void ReceiveData(DataPack pack, string eventName)
    {
        if (GameEvent.NewspaperEvent.NEWSPAPER_PUBLISHED_EVENT == eventName)
        {
            newspaperPublished = true;
        }
        else if (GameEvent.InGameUiStateEvent.ON_IN_GAME_UIS_STATE_CHANGED == eventName)
        {
            var newUiState = pack.GetValue<InGameUIState.InGameUIStateEnum>(GameEvent.InGameUiStateEvent.OnInGameUIsStateChangedData.NEW_STATE);
            if (newspaperPublished && newUiState == InGameUIState.InGameUIStateEnum.NormalState)
            {
                if (fadeTransition)
                {
                    fadeTransition.FadeIn(() =>
                    {
                        this.TriggerSliderUpdate(() =>
                        {
                            Invoke("UpdateFinished", 3f);
                        });
                    });
                }
                newspaperPublished = false;
            }
        }
    }
    public void UpdateFinished()
    {
        fadeTransition.FadeOut();
    }
}
