using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProlougeTest : MonoBehaviour
{
    [SerializeField]
    GameInstance gameplayInstance;
    public void PrologueDone()
    {
        GameMaster.GetInstance().RequestInstance(gameplayInstance);
    }
}
