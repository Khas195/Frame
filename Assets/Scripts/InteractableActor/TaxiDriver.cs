using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaxiDriver : MonoBehaviour
{
    [SerializeField]
    FadeManyTransition textBoxControl = null;
    [SerializeField]
    Text textUI = null;
    bool playerInRange = false;
    private void Start()
    {
        textBoxControl.FadeOut();
    }
    private void Update()
    {
        if (playerInRange)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                InGameUIControl.GetInstance().RequestState(InGameUIState.InGameUIStateEnum.MapState);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            textBoxControl.FadeIn();
            textUI.text = "Hey, want to go somewher' pal!";
            playerInRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            textBoxControl.FadeOut();
            playerInRange = false;
        }
    }
}
