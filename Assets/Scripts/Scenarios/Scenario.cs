using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

public class Scenario : MonoBehaviour, IObserver
{
    [SerializeField]
    public static UnityEvent<Scenario> OnScenarioEnter = new UnityEvent<Scenario>();
    [SerializeField]
    public static UnityEvent<Scenario> OnScenarioLeave = new UnityEvent<Scenario>();


    [SerializeField]
    string scenarioName = "";
    [SerializeField]
    List<GameObject> scenarioProps = new List<GameObject>();
    [SerializeField]
    List<Scenario> branches = new List<Scenario>();
    [SerializeField]
    List<ScenarioBranchCondition> conditionsToEnterThisScenario = new List<ScenarioBranchCondition>();
    [SerializeField]
    private bool isScenarioActive = false;

    private void Awake()
    {
        PostOffice.Subscribes(this, GameEvent.DaySystemEvent.DAY_CHANGED_EVENT);
    }

    private void Start()
    {
        FindChildBranches();
        if (isScenarioActive)
        {
            bool canEnterChildScenario = false;
            for (int i = 0; i < branches.Count; i++)
            {
                if (branches[i].CanAdvanceToBranch())
                {
                    LogHelper.Log(("Advance to branch " + branches[i]).Bolden().Colorize(Color.green));
                    this.LeaveScenario();
                    branches[i].EnterScenario();
                    canEnterChildScenario = true;
                    break;
                }
            }
            if (canEnterChildScenario == false)
            {
                EnterScenario();
            }
        }
    }

    public string GetScenarioName()
    {
        return scenarioName;
    }

    [Button]
    private void FindChildBranches()
    {
        branches.Clear();
        branches.AddRange(this.GetComponentsInChildren<Scenario>());
        branches.Remove(this);
    }

    private void OnDestroy()
    {
        PostOffice.Unsubscribes(this, GameEvent.DaySystemEvent.DAY_CHANGED_EVENT);
    }
    public void ReceiveData(DataPack pack, string eventName)
    {
        if (isScenarioActive)
        {
            if (eventName == GameEvent.DaySystemEvent.DAY_CHANGED_EVENT)
            {
                for (int i = 0; i < branches.Count; i++)
                {
                    if (branches[i].CanAdvanceToBranch())
                    {
                        LogHelper.Log(("Advance to branch " + branches[i]).Bolden().Colorize(Color.green));
                        this.LeaveScenario();
                        branches[i].EnterScenario();
                        return;
                    }
                }
            }
        }

    }

    public void EnterScenario()
    {
        for (int i = 0; i < scenarioProps.Count; i++)
        {
            scenarioProps[i].SetActive(true);
        }
        this.isScenarioActive = true;
        OnScenarioEnter.Invoke(this);
    }

    public void LeaveScenario()
    {
        for (int i = 0; i < scenarioProps.Count; i++)
        {
            scenarioProps[i].SetActive(false);
        }
        this.isScenarioActive = false;
    }

    private bool CanAdvanceToBranch()
    {
        for (int i = 0; i < conditionsToEnterThisScenario.Count; i++)
        {
            if (conditionsToEnterThisScenario[i].IsSatisfied() == false)
            {
                return false;
            }
        }
        return true;
    }
    public bool IsActiveScenario()
    {
        return isScenarioActive;
    }
}
