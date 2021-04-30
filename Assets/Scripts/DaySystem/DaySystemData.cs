using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DaySystemData", menuName = "Data/DaySystemData", order = 1)]
public class DaySystemData : ScriptableObject
{
    public List<int> amountOfPaperNeededPerDay = new List<int>();
}
