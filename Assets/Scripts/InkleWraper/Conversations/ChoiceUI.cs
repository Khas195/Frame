using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceUI : MonoBehaviour
{
    [SerializeField]
    Text choiceTextUI;
    [SerializeField]
    Text choiceNumber;
    [SerializeField]
    Transform choiceHolder;

    private void Update()
    {
        var xScale = choiceHolder.localScale.x;
        if (xScale > 0)
        {
            var newScale = new Vector3(1, 1, 1);
            choiceTextUI.transform.localScale = newScale;
            choiceNumber.transform.localScale = newScale;
        }
        else
        {
            var newScale = new Vector3(-1, 1, 1);
            choiceTextUI.transform.localScale = newScale;
            choiceNumber.transform.localScale = newScale;
        }
    }
    public void SetText(string text)
    {
        choiceTextUI.text = text;
    }

}
