using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

public class DiaryItem : MonoBehaviour
{
    [SerializeField]
    [Required]
    Image itemPhoto = null;
    [SerializeField]
    GameObject photoRoot = null;
    [SerializeField]
    [Required]
    Text itemText = null;
    [SerializeField]
    float photoRotMin = 0;
    [SerializeField]
    float photoRotMax = 180;
    [SerializeField]
    bool newlyAdded = true;
    [SerializeField]
    [ReadOnly]
    string textToShow = "";
    [SerializeField]
    float letterPause = 0.02f;
    [SerializeField]
    [ReadOnly]
    private string diaryStitch = "";
    [SerializeField]
    FadeManyTransition fadeTransition = null;

    [Button]
    public void RandomPhotoRotation()
    {
        var eulerRot = photoRoot.transform.eulerAngles;
        eulerRot.z = UnityEngine.Random.Range(photoRotMin, photoRotMax);
        photoRoot.transform.eulerAngles = eulerRot;

    }
    [Button]
    public void RandomSwapPhotoAndText()
    {
        var randomSwap = false;
        randomSwap = UnityEngine.Random.Range(0.0f, 1.0f) >= 0.5f ? true : false;
        if (randomSwap)
        {
            var photoPos = photoRoot.transform.position;
            var textPos = itemText.transform.position;
            var temp = photoPos;
            photoPos = textPos;
            textPos = temp;
            photoRoot.transform.position = photoPos;
            itemText.transform.position = textPos;
        }
    }

    public string GetStitch()
    {
        return diaryStitch;
    }

    public void SetPhoto(Sprite photo)
    {
        this.itemPhoto.sprite = photo;
    }

    public void SetText(string photoText)
    {
        this.textToShow = photoText;
    }

    public void SetStitch(string diaryStitch)
    {
        this.diaryStitch = diaryStitch;
    }

    public bool IsNewlyAdded()
    {
        return newlyAdded;
    }

    public void TransitionIn()
    {
        StopCoroutine("TypeText");
        LogHelper.Log("DiarySystem - Diary Item transition in");
        fadeTransition.FadeIn();
        newlyAdded = false;
        StartCoroutine("TypeText");
    }
    IEnumerator TypeText()
    {
        this.itemText.text = "";
        foreach (char letter in textToShow.ToCharArray())
        {
            itemText.text += letter;

            yield return new WaitForSeconds(letterPause);
        }
    }

    public void SkipTransition()
    {
        StopCoroutine("TypeText");
        itemText.text = textToShow;
    }
}
