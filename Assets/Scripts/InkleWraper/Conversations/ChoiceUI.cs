using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceUI : MonoBehaviour
{
    [SerializeField]
    Text choiceTextUI;
    [SerializeField]
    Transform choiceHolder;

    public void SetText(string text)
    {
        choiceTextUI.text = text;
    }

}
