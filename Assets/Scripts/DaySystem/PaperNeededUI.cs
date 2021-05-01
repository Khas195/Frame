using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

public class PaperNeededUI : MonoBehaviour
{
    [SerializeField]
    DaySystemData dayData;
    [SerializeField]
    PublishedPapersData publishedData;
    [SerializeField]
    Text textUI;

    private void Start()
    {
        UpdateTextUI();
    }
    private void Update()
    {
        UpdateTextUI();
    }

    private void UpdateTextUI()
    {
        this.textUI.text = publishedData.paperDatas.Count + " / " + dayData.amountOfPaperNeededPerDay[dayData.currentDay];
    }
}
