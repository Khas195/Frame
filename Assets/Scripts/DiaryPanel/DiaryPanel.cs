using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class DiaryPanel : MonoBehaviour
{
    [SerializeField]
    List<DiaryItem> diaryItems = null;
    [SerializeField]
    Transform diaryItemRoot = null;
    [SerializeField]
    DiaryItem prototype = null;
    [SerializeField]
    int currentPage = 1;
    [SerializeField]
    int itemPerPage = 4;
    [SerializeField]
    [ReadOnly]
    int totalPage = 0;
    private void Start()
    {
        prototype.gameObject.SetActive(false);
    }

    [Button]
    private DiaryItem AddDiaryItem()
    {
        var newDiaryItem = GameObject.Instantiate(prototype, diaryItemRoot);

        diaryItems.Add(newDiaryItem);
        newDiaryItem.RandomPhotoRotation();
        newDiaryItem.RandomSwapPhotoAndText();
        totalPage = (int)Math.Ceiling((decimal)(diaryItems.Count / (decimal)itemPerPage));
        var itemActive = IsItemOnPage(currentPage, diaryItems.Count);
        newDiaryItem.gameObject.SetActive(itemActive);
        return newDiaryItem;
    }
    public void AddDiaryItem(Sprite photo, string photoText, string diaryStitch)
    {
        var item = diaryItems.Find((DiaryItem x) => x.GetStitch() == diaryStitch);
        if (item != null)
        {
            item.SetPhoto(photo);
        }
        else
        {
            var newItem = AddDiaryItem();
            newItem.SetPhoto(photo);
            newItem.SetText(photoText);
            newItem.SetStitch(diaryStitch);
        }
    }

    private bool IsItemOnPage(int currentPage, int itemIndex)
    {
        var pageStartItemIndex = currentPage * itemPerPage;
        if (itemIndex >= pageStartItemIndex && itemIndex <= (pageStartItemIndex + itemPerPage))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Show()
    {
        diaryItemRoot.gameObject.SetActive(true);
        ShowItemsOnCurrentPage();
    }
    public void Hide()
    {
        diaryItemRoot.gameObject.SetActive(false);
    }
    [Button]
    public void NextPage()
    {
        currentPage += 1;
        if (currentPage >= totalPage)
        {
            currentPage = totalPage - 1;
        }
        ShowItemsOnCurrentPage();
    }

    [Button]
    public void PreviousPage()
    {
        currentPage -= 1;
        if (currentPage < 0)
        {
            currentPage = 0;
        }
        ShowItemsOnCurrentPage();
    }
    [Button]
    private void ShowItemsOnCurrentPage()
    {
        for (int i = 0; i < diaryItems.Count; i++)
        {
            diaryItems[i].gameObject.SetActive(false);
        }
        for (int i = currentPage * itemPerPage; i < currentPage * itemPerPage + itemPerPage; i++)
        {
            if (i >= diaryItems.Count)
            {
                return;
            }
            else
            {
                diaryItems[i].gameObject.SetActive(true);
                if (diaryItems[i].IsNewlyAdded())
                {
                    diaryItems[i].TransitionIn();
                }
            }
        }
    }

    public void AddDiaryItem(PhotoInfo newPhoto)
    {
        if (newPhoto.participants.Count > 1)
        {
            return;
        }
        var actor = newPhoto.participants[0];
        this.AddDiaryItem(newPhoto.sprite, actor.GetActorDiaryDesc(), actor.GetActorDiaryStitch());
    }

    [Button]
    private void Clear()
    {
        for (int i = diaryItems.Count - 1; i >= 0; i--)
        {
            Destroy(diaryItems[i]);
        }
        diaryItems.Clear();
        currentPage = 0;
        totalPage = 0;
    }
}
