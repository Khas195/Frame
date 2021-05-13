using UnityEngine;

[CreateAssetMenu(fileName = "MonologueConditions", menuName = "Data/MonologueConditions/SectionsHavePhotos", order = 1)]
public class SectionsHavePhotos : MonologueLineCondition
{
    [SerializeField]
    bool inversed = false;
    public override bool IsSatisfied()
    {
        var sectionHavePhotos = NewsPaperPanel.GetInstance().HavePhotoInSections();
        return inversed ? !sectionHavePhotos : sectionHavePhotos;
    }
}