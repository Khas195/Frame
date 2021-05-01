using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PublishedPapersData", menuName = "Data/PublishedPaperData", order = 1)]
public class PublishedPapersData : ScriptableObject
{
    public List<NewspaperData> paperDatas = new List<NewspaperData>();

    public void Reset()
    {
        paperDatas.Clear();
    }
}
