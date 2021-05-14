using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaxiMapControl : MonoBehaviour
{
    [SerializeField]
    GameObject mapPanel = null;
    private void Start()
    {
        HideMap();
    }
    public void ShowMap()
    {
        mapPanel.SetActive(true);
    }
    public void HideMap()
    {
        mapPanel.SetActive(false);
    }
    public void LoadInstance(GameInstance instance)
    {
        GameMaster.GetInstance().RequestInstance(instance);
        InGameUIControl.GetInstance().RequestState(InGameUIState.InGameUIStateEnum.NormalState);
    }
}
