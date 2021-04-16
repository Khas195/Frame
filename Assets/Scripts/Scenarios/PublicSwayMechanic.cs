using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class PublicSwayMechanic : SingletonMonobehavior<PublicSwayMechanic>
{
    [SerializeField]
    Color proCommunistColor;
    [SerializeField]
    Color proCapitalistColor;
    [SerializeField]
    Color neutralColor;

    [SerializeField]
    List<ScenarioActor> cititzens;

    [SerializeField]
    List<ScenarioActor> scenarioActors;
    [SerializeField]
    GameObject citizenRoot = null;

    [SerializeField]
    GameObject scenarioRoot = null;

    [SerializeField]
    [MinMaxSlider(0, 100)]
    [OnValueChanged("RoundUpSwayPercentage")]
    [InfoBox("LHS = Communist, Middle = Neutral, RHS = Capitalist")]
    Vector2 swayPercentage;

    [SerializeField]
    [InfoBox("LHS = Communist, Middle = Neutral, RHS = Capitalist")]
    [MinMaxSlider(0, 200)]
    [OnValueChanged("OnInFluenceChanged")]
    Vector2 influence;

    [ShowNonSerializedField]
    int capitalist;
    [ShowNonSerializedField]
    int communist;
    [ShowNonSerializedField]
    int neutral;
    private void Start()
    {
        cititzens.Clear();
        scenarioActors.Clear();
        GatherActors();
        AssignActorsAccordingToSway(true);
    }
    public void AddInfluence(float value, ScenarioActor.ActorFaction targetFaction)
    {
        if (targetFaction == ScenarioActor.ActorFaction.Capitalist)
        {
            influence.y -= value;
        }
        else if (targetFaction == ScenarioActor.ActorFaction.Communist)
        {
            influence.x += value;
        }
        OnInFluenceChanged();
    }
    public void OnInFluenceChanged()
    {
        influence = new Vector2(Mathf.RoundToInt(influence.x), Mathf.RoundToInt(influence.y));
        UpdateSway();
    }

    private void UpdateSway()
    {
        swayPercentage.x = (influence.x / 200) * 100;
        swayPercentage.y = (influence.y / 200) * 100;
        RoundUpSwayPercentage();
    }

    public void RoundUpSwayPercentage()
    {
        swayPercentage = new Vector2(Mathf.RoundToInt(swayPercentage.x), Mathf.RoundToInt(swayPercentage.y));
    }
    [Button]
    public void AssignActorsAccordingToSway(bool fastTransition = false)
    {
        int targetCommie = Mathf.RoundToInt((swayPercentage.x / 100.0f) * cititzens.Count);
        int targetCapitalist = Mathf.RoundToInt(((100 - swayPercentage.y) / 100.0f) * cititzens.Count);

        int curCommie = 0;
        int curCapital = 0;
        cititzens = Util.Shuffle<ScenarioActor>(cititzens);
        for (int i = 0; i < cititzens.Count; i++)
        {
            if (curCommie < targetCommie)
            {
                if (cititzens[i].GetFaction() != ScenarioActor.ActorFaction.Communist)
                {
                    cititzens[i].AssignFaction(ScenarioActor.ActorFaction.Communist, fastTransition);
                }
                curCommie += 1;
            }
            else if (curCapital < targetCapitalist)
            {
                if (cititzens[i].GetFaction() != ScenarioActor.ActorFaction.Capitalist)
                {
                    cititzens[i].AssignFaction(ScenarioActor.ActorFaction.Capitalist, fastTransition);
                }
                curCapital += 1;

            }
            else
            {
                cititzens[i].AssignFaction(ScenarioActor.ActorFaction.Neutral);
            }
        }
        communist = curCommie;
        capitalist = curCapital;
        neutral = cititzens.Count - curCapital - curCommie;

    }

    public void AssignPhotoInfluence(ref PhotoInfo newPhoto)
    {
        for (int i = 0; i < scenarioActors.Count; i++)
        {
            if (scenarioActors[i].IsOnCamera())
            {
                newPhoto.capitalistInfluence += scenarioActors[i].GetCapitalInfluence();
                newPhoto.communistInfluence += scenarioActors[i].GetCommunistInfluence();
            }
        }
    }

    [Button]
    public void GatherActors()
    {
        cititzens.AddRange(citizenRoot.GetComponentsInChildren<ScenarioActor>());
        scenarioActors.AddRange(scenarioRoot.GetComponentsInChildren<ScenarioActor>());
    }
    private int GetAmoutOfActorOfFaction(ScenarioActor.ActorFaction factionToCheck)
    {
        var curActorToFaction = 0;
        for (int i = 0; i < cititzens.Count; i++)
        {
            if (cititzens[i].GetFaction() == factionToCheck)
            {
                curActorToFaction += 1;
            }
        }

        return curActorToFaction;
    }

    public Color GetColorToFaction(ScenarioActor.ActorFaction newFaction)
    {
        if (newFaction == ScenarioActor.ActorFaction.Capitalist)
        {
            return this.proCapitalistColor;
        }
        else if (newFaction == ScenarioActor.ActorFaction.Communist)
        {
            return this.proCommunistColor;
        }
        else
        {
            return this.neutralColor;
        }
    }
}
