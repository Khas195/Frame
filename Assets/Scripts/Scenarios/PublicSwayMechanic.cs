using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
public class PublicSwayMechanic : SingletonMonobehavior<PublicSwayMechanic>, IObserver
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
    [Expandable]
    PublicSwayData currentSwayData;
    [SerializeField]
    [Expandable]
    PublicSwayData startGameSwayData;

    [ShowNonSerializedField]
    int capitalist;
    [ShowNonSerializedField]
    int communist;
    [ShowNonSerializedField]
    int neutral;
    protected override void Awake()
    {
        base.Awake();
        cititzens.Clear();
        scenarioActors.Clear();
        PostOffice.Subscribes(this, GameEvent.MapChangedEvent.MAP_CHANGED_EVENT);
    }
    private void OnDestroy()
    {
        PostOffice.Unsubscribes(this, GameEvent.MapChangedEvent.MAP_CHANGED_EVENT);
    }
    private void Start()
    {
        currentSwayData.swayPercentage = startGameSwayData.swayPercentage;
        currentSwayData.influence = startGameSwayData.influence;
        AssignActorsAccordingToSway(true);
    }
    public void AddInfluence(float value, ScenarioActor.ActorFaction targetFaction)
    {
        if (targetFaction == ScenarioActor.ActorFaction.Capitalist)
        {
            currentSwayData.influence.y -= value;
        }
        else if (targetFaction == ScenarioActor.ActorFaction.Communist)
        {
            currentSwayData.influence.x += value;
        }
        OnInFluenceChanged();
    }
    public void OnInFluenceChanged()
    {
        currentSwayData.influence = new Vector2(Mathf.RoundToInt(currentSwayData.influence.x), Mathf.RoundToInt(currentSwayData.influence.y));
        UpdateSway();
    }

    private void UpdateSway()
    {
        currentSwayData.swayPercentage.x = (currentSwayData.influence.x / 200) * 100;
        currentSwayData.swayPercentage.y = (currentSwayData.influence.y / 200) * 100;
        RoundUpSwayPercentage();
    }

    public void RoundUpSwayPercentage()
    {
        currentSwayData.swayPercentage = new Vector2(Mathf.RoundToInt(currentSwayData.swayPercentage.x), Mathf.RoundToInt(currentSwayData.swayPercentage.y));
    }
    [Button]
    public void AssignActorsAccordingToSway(bool fastTransition = false)
    {
        int targetCommie = Mathf.RoundToInt((currentSwayData.swayPercentage.x / 100.0f) * cititzens.Count);
        int targetCapitalist = Mathf.RoundToInt(((100 - currentSwayData.swayPercentage.y) / 100.0f) * cititzens.Count);

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
                newPhoto.participants.Add(scenarioActors[i]);
            }
        }
    }


    public void RegisterCitizen(ScenarioActor newCitizen)
    {
        this.cititzens.Add(newCitizen);
    }
    public void RegisterScenarioActors(ScenarioActor newScenarioActor)
    {
        this.scenarioActors.Add(newScenarioActor);
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

    public void ReceiveData(DataPack pack, string eventName)
    {
        if (eventName == GameEvent.MapChangedEvent.MAP_CHANGED_EVENT)
        {
            this.CleanActorLists();
        }
    }

    private void CleanActorLists()
    {
        for (int i = scenarioActors.Count - 1; i >= 0; i--)
        {
            if (scenarioActors[i] == null)
            {
                scenarioActors.Remove(scenarioActors[i]);
            }
        }

        for (int i = cititzens.Count - 1; i >= 0; i--)
        {
            if (cititzens[i] == null)
            {
                cititzens.Remove(cititzens[i]);
            }
        }
    }
}
