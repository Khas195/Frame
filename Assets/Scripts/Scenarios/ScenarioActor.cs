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
    int point;
    [SerializeField]
    SpriteRenderer sprite;
    [SerializeField]
    ActorFaction curFaction = ActorFaction.Neutral;
    [SerializeField]
    int capitalInfluence = 0;
    [SerializeField]
    int communistInfluence = 0;

    public int Point { get => point; }

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

    public float GetCapitalInfluence()
    {
        return capitalInfluence;
    }

    public float GetCommunistInfluence()
    {
        return communistInfluence;
    }
}
