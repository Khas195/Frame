using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface IPhotoParticipant
{
    int GetCapitalInfluence();
    int GetCommunistInfluence();
    string GetStoryStitch();
    string GetDescription();
    void ResetInfluences();
    bool IsOnCamera();
}

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
                influence += participants[i].GetCommunistInfluence();
            }
            return influence;
        }
    }
    public List<IPhotoParticipant> participants = new List<IPhotoParticipant>();
    public void ClearInfo()
    {
        sprite = null;
        participants.Clear();
    }

}
