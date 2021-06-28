using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ScenarioConditionsData", menuName = "Data/ScenarioConditions/InfluenceCondition", order = 1)]
public class InfluenceCondition : ScenarioBranchCondition
{
	public enum CompareInfluence
	{
		Bigger,
		Smaller
	}
	[Serializable]
	public class InfluenceCompareTo
	{
		public ScenarioActor.ActorFaction faction;
		public int numberToCompare;
		public CompareInfluence compareMethod;
	}
	[SerializeField]
	InfluenceCompareTo influence;
	[SerializeField]
	PublicSwayData data;
	public override bool IsSatisfied()
	{
		float influenceNumber = 100 - data.swayPercentage.y;
		if (influence.faction == ScenarioActor.ActorFaction.Communist)
		{
			influenceNumber = data.swayPercentage.x;
		}
		else if (influence.faction == ScenarioActor.ActorFaction.Neutral)
		{
			influenceNumber = 100 - influenceNumber - data.swayPercentage.x;
		}

		if (influence.compareMethod == CompareInfluence.Bigger)
		{
			return influenceNumber > influence.numberToCompare;
		}
		else if (influence.compareMethod == CompareInfluence.Smaller)
		{
			return influenceNumber < influence.numberToCompare;
		}

		return false;
	}
}