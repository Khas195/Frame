using UnityEngine;

[CreateAssetMenu(fileName = "ScenarioConditionsData", menuName = "Data/ScenarioConditions/MixedCondition", order = 1)]
public class MixedCondition : ScenarioBranchCondition
{
	[SerializeField]
	PublicSwayData data;
	[SerializeField]
	float differenceAmount;
	public override bool IsSatisfied()
	{

		if (Mathf.Abs(data.swayPercentage.x - (100 - data.swayPercentage.y)) <= differenceAmount)
		{
			return true;
		}
		return false;
	}
}
