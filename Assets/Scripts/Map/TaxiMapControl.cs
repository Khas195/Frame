using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaxiMapControl : MonoBehaviour, IObserver
{
    [SerializeField]
    GameObject mapPanel = null;
    [SerializeField]
    AudioSource carDoorOpen = null;
    [SerializeField]
    AudioSource carDoorClose = null;
    [SerializeField]
    Image playerIcon = null;
    [SerializeField]
    Transform picisLocation = null;
    [SerializeField]
    Transform parkLocation = null;
    [SerializeField]
    GameObject mapNormal = null;
    [SerializeField]
    GameObject mapBroken = null;
    [SerializeField]
    GameObject parkButtonNormal = null;
    [SerializeField]
    GameObject parkBrokenButton = null;
    [SerializeField]
    List<ScenarioBranchCondition> conditionsToChangeMap = new List<ScenarioBranchCondition>();
    private void Start()
    {
        HideMap();
        PostOffice.Subscribes(this, GameEvent.MapChangedEvent.MAP_CHANGED_EVENT);
        PostOffice.Subscribes(this, GameEvent.NewspaperEvent.NEWSPAPER_PUBLISHED_EVENT);
        playerIcon.transform.position = picisLocation.position;
        mapNormal.gameObject.SetActive(true);
        mapBroken.gameObject.SetActive(false);
        parkButtonNormal.gameObject.SetActive(true);
        parkBrokenButton.gameObject.SetActive(false);
    }
    public void ShowMap()
    {
        mapPanel.SetActive(true);
    }
    public void HideMap()
    {
        mapPanel.SetActive(false);
    }
    public void ChangeMapToBroken()
    {
        LogHelper.Log("Map Control - Map broken");
        this.mapNormal.gameObject.SetActive(false);
        this.mapBroken.gameObject.SetActive(true);
        parkButtonNormal.gameObject.SetActive(false);
        parkBrokenButton.gameObject.SetActive(true);
    }
    public void LoadInstance(GameInstance instance)
    {
        GameMaster.GetInstance().RequestInstance(instance);
        InGameUIControl.GetInstance().RequestState(InGameUIState.InGameUIStateEnum.NormalState);
        carDoorOpen.Play();
        carDoorClose.PlayDelayed(carDoorOpen.clip.length);
    }

    public void ReceiveData(DataPack pack, string eventName)
    {
        if (eventName.Equals(GameEvent.MapChangedEvent.MAP_CHANGED_EVENT))
        {
            var currentLocaton = pack.GetValue<GameEvent.MapChangedEvent.MapLocation>(GameEvent.MapChangedEvent.MAP_LOCATION_DATA);
            if (currentLocaton == GameEvent.MapChangedEvent.MapLocation.PicisStreet)
            {
                playerIcon.transform.position = picisLocation.position;
            }
            else
            {
                playerIcon.transform.position = parkLocation.position;
            }
        }
        if (eventName.Equals(GameEvent.NewspaperEvent.NEWSPAPER_PUBLISHED_EVENT))
        {
            bool condition = true;

            conditionsToChangeMap.ForLoop<ScenarioBranchCondition>((ScenarioBranchCondition curCondition) =>
            {
                if (curCondition.IsSatisfied() == false)
                {
                    condition = false;
                }
            });

            if (condition == false)
            {
                ChangeMapToBroken();
            }
        }
    }
}
