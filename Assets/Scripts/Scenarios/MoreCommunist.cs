using UnityEngine;

[CreateAssetMenu(fileName = "ScenarioConditionsData", menuName = "Data/ScenarioConditions/MoreCommunist", order = 1)]
public class MoreCommunist : ScenarioBranchCondition
{
	[SerializeField]
	PublicSwayData data;
	public override bool IsSatisfied()
	{
		if (data.swayPercentage.x > (100 - data.swayPercentage.y))
		{
			return true;
		}
		return false;
	}
}
