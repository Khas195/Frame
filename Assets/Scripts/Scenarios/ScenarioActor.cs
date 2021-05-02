using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class ScenarioActor : MonoBehaviour
{
    public enum ActorFaction
    {
        Neutral,
        Communist,
        Capitalist
    }
    [SerializeField]
    bool isFactionFixed = false;
    [SerializeField]
    [HideIf("isFactionFixed")]
    SpriteRenderer highlightSprite;
    [SerializeField]
    SpriteRenderer characterSprite;
    [SerializeField]
    ActorFaction curFaction = ActorFaction.Neutral;
    [SerializeField]
    int capitalInfluence = 0;
    [SerializeField]
    int communistInfluence = 0;
    [SerializeField]
    bool isOnCamera = false;
    [SerializeField]
    Color previousColor;
    [SerializeField]
    Color targetColor;
    [SerializeField]
    float transitionTime = 1.0f;
    float curTime = 100;

    private void Start()
    {
        if (isFactionFixed == false)
        {
            ChangeColorToFaction();
        }
    }

    private void Update()
    {
        isOnCamera = characterSprite.isVisible;
        if (isFactionFixed == false)
        {
            if (curTime <= transitionTime)
            {
                this.highlightSprite.color = Color.Lerp(previousColor, targetColor, curTime / transitionTime);
                curTime += Time.deltaTime;
            }
            else
            {
                this.highlightSprite.color = targetColor;
            }
        }

    }
    public void SetColorOfActor(Color color)
    {
        highlightSprite.color = color;
    }
    public void AssignFaction(ActorFaction newFaction, bool instant = true)
    {
        this.curFaction = newFaction;
        previousColor = this.highlightSprite.color;
        targetColor = PublicSwayMechanic.GetInstance().GetColorToFaction(newFaction);
        if (instant == false)
        {
            curTime = 0;
        }
        else
        {
            curTime = 100f;
            this.SetColorOfActor(targetColor);
        }
    }
    public ActorFaction GetFaction()
    {
        return curFaction;
    }
    [Button]
    public void ChangeColorToFaction()
    {

        this.SetColorOfActor(PublicSwayMechanic.GetInstance().GetColorToFaction(curFaction));
        previousColor = this.highlightSprite.color;
        targetColor = this.highlightSprite.color;
    }

    public bool IsOnCamera()
    {
        return isOnCamera;
    }

    public int GetCapitalInfluence()
    {
        return capitalInfluence;
    }

    public int GetCommunistInfluence()
    {
        return communistInfluence;
    }

    public void ResetInfluences()
    {
        this.capitalInfluence = 0;
        this.communistInfluence = 0;
    }
}
