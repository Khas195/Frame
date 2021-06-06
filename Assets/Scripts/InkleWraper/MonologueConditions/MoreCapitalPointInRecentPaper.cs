using UnityEngine;

[CreateAssetMenu(fileName = "MonologueConditions", menuName = "Data/MonologueConditions/MoreCapitalPointInRecentPaper", order = 1)]
public class MoreCapitalPointInRecentPaper : MonologueLineCondition, IObserver
{
    [SerializeField]
    bool inverse = false;
    private int commiePoint;
    private int capitalPoint;
    private void OnEnable()
    {
        LogHelper.Log("ScriptableObject Start " + this);
        PostOffice.Subscribes(this, GameEvent.NewspaperEvent.NEWSPAPER_PUBLISHED_EVENT);
    }
    private void OnDisable()
    {
        PostOffice.Unsubscribes(this, GameEvent.NewspaperEvent.NEWSPAPER_PUBLISHED_EVENT);
    }
    public override bool IsSatisfied()
    {
        if (inverse == false)
        {
            if (capitalPoint > commiePoint)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            if (commiePoint > capitalPoint)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public void ReceiveData(DataPack pack, string eventName)
    {
        if (eventName == GameEvent.NewspaperEvent.NEWSPAPER_PUBLISHED_EVENT)
        {
            commiePoint = pack.GetValue<int>(GameEvent.NewspaperEvent.PaperPublishedData.TOTAL_COMMIE_POINT);
            capitalPoint = pack.GetValue<int>(GameEvent.NewspaperEvent.PaperPublishedData.TOTAL_CAPITAL_POINT);
        }
    }
}
