using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DaySystemData", menuName = "Data/DaySystemData", order = 1)]
public class DaySystemData : ScriptableObject
{
    public int currentDay = 0;
    public List<int> amountOfPaperNeededPerDay = new List<int>();
    public void ResetDay()
    {
        currentDay = 0;
    }
}
