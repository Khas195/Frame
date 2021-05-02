using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

public class SwayStatsPanel : MonoBehaviour
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
    [ReadOnly]
    float currentTime = 0.0f;
    [SerializeField]
    [ReadOnly]
    bool isTransitioning = false;
    private float startLeft;
    private float startRight;
    private float endLeft;
    private float endRight;

    private void Awake()
    {
        yesterdayData.swayPercentage = new Vector2(0, 100);
    }
    [Button]
    public void TriggerSliderUpdate()
    {
        commieSlider.color = PublicSwayMechanic.GetInstance().GetColorToFaction(ScenarioActor.ActorFaction.Communist);
        sliderBG.color = PublicSwayMechanic.GetInstance().GetColorToFaction(ScenarioActor.ActorFaction.Neutral);
        capitalistSlider.color = PublicSwayMechanic.GetInstance().GetColorToFaction(ScenarioActor.ActorFaction.Capitalist);

        startLeft = yesterdayData.swayPercentage.x / 100;
        startRight = (100 - yesterdayData.swayPercentage.y) / 100;
        endLeft = (currentSwayData.swayPercentage.x / 100);
        endRight = ((100 - currentSwayData.swayPercentage.y) / 100);
        yesterdayData.swayPercentage = currentSwayData.swayPercentage;

        commieSlider.fillAmount = startLeft;
        capitalistSlider.fillAmount = startRight;
        isTransitioning = true;
        currentTime = 0;
    }
    private void Update()
    {
        if (isTransitioning)
        {
            if (currentTime <= transitionTime)
            {
                commieSlider.fillAmount = Mathf.Lerp(startLeft, endLeft, currentTime / transitionTime);
                capitalistSlider.fillAmount = Mathf.Lerp(startRight, endRight, currentTime / transitionTime);

                if (currentTime >= transitionTime)
                {
                    isTransitioning = false;
                }

                currentTime += Time.deltaTime;
            }
        }

    }
}
