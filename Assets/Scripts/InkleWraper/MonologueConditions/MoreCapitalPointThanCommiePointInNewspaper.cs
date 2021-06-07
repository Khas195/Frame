using UnityEngine;

[CreateAssetMenu(fileName = "MonologueConditions", menuName = "Data/MonologueConditions/MoreCapitalPointThanCommiePointInNewspaper", order = 1)]
public class MoreCapitalPointThanCommiePointInNewspaper : MonologueLineCondition
{
    [SerializeField]
    bool inversed = false;

    public override bool IsSatisfied()
    {
        var totalCapitalPoint = NewsPaperPanel.GetInstance().GetTotalCapitalistPointFromSections();
        var totalCommiePoint = NewsPaperPanel.GetInstance().GetTotalCommiePointFromSections();
        return inversed == false ? totalCapitalPoint > totalCommiePoint : totalCommiePoint > totalCapitalPoint;
    }
}
