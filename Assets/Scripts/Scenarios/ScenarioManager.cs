using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

public class ScenarioManager : SingletonMonobehavior<ScenarioManager>
{
    [SerializeField]
    List<Scenario> scenarios = new List<Scenario>();
    [SerializeField]
    Transform scenariosRoot = null;

    private void Start()
    {
        scenarios.AddRange(scenariosRoot.GetComponentsInChildren<Scenario>());
    }
    public Scenario GetActiveScenario()
    {
        Scenario result = null;
        scenarios.ForLoop<Scenario>((Scenario curScenario) =>
        {
            if (curScenario.IsActiveScenario())
            {
                result = curScenario;
                return;
            }
        });
        return result;
    }

}
