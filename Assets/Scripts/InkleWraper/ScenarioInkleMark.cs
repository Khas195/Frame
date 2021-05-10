using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenarioInkleMark : MonoBehaviour
{
    [SerializeField]
    Scenario mScenario;
    [SerializeField]
    string inkleStitch;
    private void Awake()
    {
        Scenario.OnScenarioEnter.AddListener(this.AddPaperboyLines);

    }

    public void AddPaperboyLines(Scenario newScenario)
    {
        if (newScenario == mScenario)
        {
            InkleManager.GetInstance().AddPaperboyLines(inkleStitch);
        }
    }
}
