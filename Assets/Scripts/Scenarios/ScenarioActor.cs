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
    SpriteRenderer sprite;
    [SerializeField]
    ActorFaction curFaction = ActorFaction.Neutral;
    [SerializeField]
    int capitalInfluence = 0;
    [SerializeField]
    int communistInfluence = 0;
    [SerializeField]
    bool isOnCamera = false;


    private void Update()
    {
        isOnCamera = sprite.isVisible;
    }
    public void SetColorOfActor(Color color)
    {
        sprite.color = color;
    }
    public void AssignFaction(ActorFaction newFaction)
    {
        this.curFaction = newFaction;
        this.SetColorOfActor(PublicSwayMechanic.GetInstance().GetColorToFaction(newFaction));
    }
    public ActorFaction GetFaction()
    {
        return curFaction;
    }
    [Button]
    public void ChangeColorToFaction()
    {
        this.SetColorOfActor(PublicSwayMechanic.GetInstance().GetColorToFaction(curFaction));
    }
    [Button]
    public void AffectInfluence()
    {
        PublicSwayMechanic.GetInstance().AddInfluence(capitalInfluence, ActorFaction.Capitalist);
        PublicSwayMechanic.GetInstance().AddInfluence(communistInfluence, ActorFaction.Communist);
    }

    public bool IsOnCamera()
    {
        return this.sprite.isVisible;
    }

    public int GetCapitalInfluence()
    {
        return capitalInfluence;
    }

    public int GetCommunistInfluence()
    {
        return communistInfluence;
    }
}
