using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController2D : MonoBehaviour
{
    [SerializeField]
    Character2D character = null;
    [SerializeField]
    UnityEvent hackTrigger = new UnityEvent();
    [SerializeField]
    UnityEvent interactTrigger = new UnityEvent();
    // Update is called once per frame
    void Update()
    {
        var side = Input.GetAxisRaw("Horizontal");
        var forward = Input.GetAxisRaw("Vertical");
        if (Input.GetKeyDown(KeyCode.E))
        {
            interactTrigger.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            hackTrigger.Invoke();
        }
        character.Move(side, forward);
    }

    public void SetCharacter(Character2D targetCharacter)
    {
        character = targetCharacter;
    }
}
