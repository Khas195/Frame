using UnityEngine;

[CreateAssetMenu(fileName = "ScenarioConditionsData", menuName = "Data/ScenarioConditions/DayCondition", order = 1)]
public class DayCheckCondition : ScenarioBranchCondition
{
    [SerializeField]
    DaySystemData data;
    [SerializeField]
    int dayToCheck = 0;
    public override bool IsSatisfied()
    {
        return data.currentDay >= dayToCheck - 1;
    }
}