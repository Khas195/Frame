using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class PhotoInfo
{
    public Sprite sprite;
    public int CapitalistInfluence
    {
        get
        {
            var influence = 0;
            for (int i = 0; i < participants.Count; i++)
            {
                influence += participants[i].GetCapitalInfluence();
            }
            return influence;
        }
    }
    public int CommunistInfluence
    {
        get
        {
            var influence = 0;
            for (int i = 0; i < participants.Count; i++)
            {
                influence += participants[i].GetCapitalInfluence();
            }
            return influence;
        }
    }
    public List<ScenarioActor> participants = new List<ScenarioActor>();
    public void ClearInfo()
    {
        sprite = null;
        participants.Clear();
    }

}
