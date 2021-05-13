using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MonologueConditions", menuName = "Data/MonologueConditions/HasPhotoCondition", order = 1)]
public class HasPhotoCondition : MonologueLineCondition
{
    [SerializeField]
    bool inverse = false;
    public override bool IsSatisfied()
    {
        bool hasPhoto = NewsPaperPanel.GetInstance().HasPhoto();
        return inverse == true ? hasPhoto : !hasPhoto;
    }
}
