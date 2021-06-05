using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class ScenarioProps : MonoBehaviour
{
    [SerializeField]
    List<GameObject> scenarioProps = new List<GameObject>();

    [SerializeField]
    string scenarioName = "";
    [SerializeField]
    [ReadOnly]
    bool parkPropsOn = false;
    private void Start()
    {
        Scenario.OnScenarioEnter.AddListener(this.OnScenarioEnter);
        Scenario.OnScenarioLeave.AddListener(this.OnScenarioExit);
        var activeScenario = ScenarioManager.GetInstance().GetActiveScenario();
        if (activeScenario.GetScenarioName() == this.scenarioName)
        {
            TurnOnProps();
        }
        else
        {
            TurnOffProps();
        }
    }
    public void OnScenarioEnter(Scenario scenario)
    {
        if (scenario.GetScenarioName() == this.scenarioName)
        {
            this.TurnOnProps();
        }

    }
    public void OnScenarioExit(Scenario scenario)
    {
        if (scenario.GetScenarioName() == this.scenarioName)
        {
            this.TurnOffProps();
        }
    }
    [Button]
    public void TurnOnProps()
    {
        for (int i = 0; i < scenarioProps.Count; i++)
        {
            scenarioProps[i].SetActive(true);
        }
        parkPropsOn = true;
    }
    [Button]
    public void TurnOffProps()
    {
        for (int i = 0; i < scenarioProps.Count; i++)
        {
            scenarioProps[i].SetActive(false);
        }
        parkPropsOn = false;
    }
}
