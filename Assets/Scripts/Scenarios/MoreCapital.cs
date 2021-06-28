using UnityEngine;

[CreateAssetMenu(fileName = "ScenarioConditionsData", menuName = "Data/ScenarioConditions/MoreCapital", order = 1)]
public class MoreCapital : ScenarioBranchCondition
{
	[SerializeField]
	PublicSwayData data;
	public override bool IsSatisfied()
	{
		if (data.swayPercentage.x <= (100 - data.swayPercentage.y))
		{
			return true;
		}
		return false;
	}
}