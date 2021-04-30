using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PublishedPaper : MonoBehaviour
{
    [SerializeField]
    Image mainArticle;
    [SerializeField]
    Image leftArticle;
    [SerializeField]
    Image rightArticle;
    public void SetData(NewspaperData data)
    {
        mainArticle.sprite = data.mainArticle;
        leftArticle.sprite = data.leftArticle;
        rightArticle.sprite = data.rightArticle;
    }
}
