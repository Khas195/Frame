using UnityEngine;

[CreateAssetMenu(fileName = "MonologueConditions", menuName = "Data/MonologueConditions/NewspaperPointConditions", order = 1)]
public class TotalNewspaperPointCondition : MonologueLineCondition
{
    [SerializeField]
    [NaughtyAttributes.InfoBox("Checking against the difference between (capital Point - Commie Points)", NaughtyAttributes.EInfoBoxType.Normal)]
    int totalPointSumRangeToCheck;
    [SerializeField]
    [NaughtyAttributes.InfoBox("If uncheck, check for smaller or equal", NaughtyAttributes.EInfoBoxType.Normal)]
    bool checkBiggerOrEqual = true;

    public override bool IsSatisfied()
    {
        var totalCapitalPoint = NewsPaperPanel.GetInstance().GetTotalCapitalistPointFromSections();
        var totalCommiePoint = NewsPaperPanel.GetInstance().GetTotalCommiePointFromSections();
        if (checkBiggerOrEqual)
        {
            return Mathf.Abs(totalCapitalPoint - totalCommiePoint) >= totalPointSumRangeToCheck;
        }
        else
        {
            return Mathf.Abs(totalCapitalPoint - totalCommiePoint) <= totalPointSumRangeToCheck;
        }
    }
}
