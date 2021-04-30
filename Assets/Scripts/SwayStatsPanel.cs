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
    private void Start()
    {
        yesterdayData.swayPercentage = new Vector2(0, 100);
        commieSlider.color = PublicSwayMechanic.GetInstance().GetColorToFaction(ScenarioActor.ActorFaction.Capitalist);
        sliderBG.color = PublicSwayMechanic.GetInstance().GetColorToFaction(ScenarioActor.ActorFaction.Neutral);
        capitalistSlider.color = PublicSwayMechanic.GetInstance().GetColorToFaction(ScenarioActor.ActorFaction.Communist);
    }
    public void UpdateSlider()
    {
        commieSlider.fillAmount = yesterdayData.swayPercentage.x / 100;
        capitalistSlider.fillAmount = (100 - yesterdayData.swayPercentage.y) / 100;
        isTransitioning = true;
        currentTime = 0;
    }
    private void Update()
    {
        if (isTransitioning)
        {
            if (currentTime <= transitionTime)
            {
                commieSlider.fillAmount = Mathf.Lerp(yesterdayData.swayPercentage.x / 100, currentSwayData.swayPercentage.x / 100, currentTime / transitionTime);
                capitalistSlider.fillAmount = Mathf.Lerp((100 - yesterdayData.swayPercentage.y) / 100, (100 - currentSwayData.swayPercentage.y) / 100, currentTime / transitionTime);

                if (currentTime >= transitionTime)
                {
                    isTransitioning = false;
                    yesterdayData.swayPercentage = currentSwayData.swayPercentage;
                }

                currentTime += Time.deltaTime;
            }
        }

    }
}
