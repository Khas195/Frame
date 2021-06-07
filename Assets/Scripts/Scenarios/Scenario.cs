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
    List<Scenario> branches = new List<Scenario>();
    [SerializeField]
    List<ScenarioBranchCondition> conditionsToEnterThisScenario = new List<ScenarioBranchCondition>();
    [SerializeField]
    [ReadOnly]
    private bool isScenarioActive = false;

    private void Start()
    {
        PostOffice.Subscribes(this, GameEvent.DaySystemEvent.DAY_CHANGED_EVENT);
        FindChildBranches();
        if (isScenarioActive)
        {
            bool canEnterChildScenario = TryToAdvanceToBranches();
            if (canEnterChildScenario == false)
            {
                EnterScenario();
            }
        }
    }
    private void OnDestroy()
    {
        PostOffice.Unsubscribes(this, GameEvent.DaySystemEvent.DAY_CHANGED_EVENT);
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


    public void ReceiveData(DataPack pack, string eventName)
    {
        if (isScenarioActive)
        {
            if (eventName == GameEvent.DaySystemEvent.DAY_CHANGED_EVENT)
            {
                TryToAdvanceToBranches();
            }
        }

    }
    public bool TryToAdvanceToBranches()
    {
        LogHelper.Log("Scenario - Checking available branches to advance for " + this);
        for (int i = 0; i < branches.Count; i++)
        {
            LogHelper.Log("Scenario - Checking " + branches[i] + ".");
            if (branches[i].CanAdvanceToBranch())
            {
                LogHelper.Log(("Scenario - Advance to branch " + branches[i]).Bolden().Colorize(Color.green));
                this.LeaveScenario();
                branches[i].EnterScenario();
                return true;
            }
        }
        return false;
    }
    public void EnterScenario()
    {
        bool continueToBranch = false;
        continueToBranch = TryToAdvanceToBranches();
        if (continueToBranch == true) return;
        this.isScenarioActive = true;
        OnScenarioEnter.Invoke(this);
    }

    public void LeaveScenario()
    {
        OnScenarioLeave.Invoke(this);
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
