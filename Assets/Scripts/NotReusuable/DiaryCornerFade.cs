using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiaryCornerFade : MonoBehaviour
{
    [SerializeField]
    Image image;

    private void Start()
    {
        var color = image.color;
        color.a = 0;
        image.color = color;

    }

    public void OnMouseEnter()
    {
        var color = image.color;
        color.a = 1;
        image.color = color;
    }
    public void OnMouseExit()
    {
        var color = image.color;
        color.a = 0;
        image.color = color;
    }
}
