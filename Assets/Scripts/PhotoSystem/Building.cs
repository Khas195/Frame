using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class Building : MonoBehaviour, IPhotoParticipant
{
    [SerializeField]
    SpriteRenderer spriteRender = null;
    [SerializeField]
    string inkStitch = "RandomBuilding";
    [SerializeField]
    [ReadOnly]
    string inkDesc = "No Description";
    private void Start()
    {
        PublicSwayMechanic.GetInstance().RegisterBuilding(this);
        inkDesc = InkleManager.GetInstance().RequestActorDesc(inkStitch);
    }
    [Button]
    public void FindSpriteRenderInSelf()
    {
        this.spriteRender = this.GetComponent<SpriteRenderer>();
    }
    public int GetCapitalInfluence()
    {
        return 0;
    }
    public int GetCommunistInfluence()
    {
        return 0;
    }

    public string GetDescription()
    {
        return inkDesc;
    }

    public string GetStoryStitch()
    {
        return inkStitch;
    }

    public bool IsOnCamera()
    {
        return this.spriteRender.isVisible;
    }

    public void ResetInfluences()
    {
        return;
    }

}
