using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

public class PhotoListManager : MonoBehaviour
{
    [SerializeField]
    int contentPerPage;
    [SerializeField]
    int currentPage = 0;
    [SerializeField]
    List<PhotoHolder> photos;
    [SerializeField]
    Transform contentRoot;



    [SerializeField]
    GameObject photoExample;
    [SerializeField]
    GameObject photoListRoot = null;

    public void Hide()
    {
        photoListRoot.SetActive(false);
    }
    public void Show()
    {
        photoListRoot.SetActive(true);
    }
    // Start is called before the first frame update
    void Start()
    {

    }


    public void AddPhoto(PhotoInfo photoInfo)
    {
        var newGameObject = GameObject.Instantiate(photoExample, contentRoot);
        var photoHolder = newGameObject.GetComponent<PhotoHolder>();
        photoHolder.SetPhotoInfo(photoInfo);
        photos.Add(photoHolder);
        float totalPage = photos.Count / (contentPerPage);
        if (currentPage == totalPage)
        {
            newGameObject.SetActive(true);
        }
    }
    [Button]
    public void NextPage()
    {
        float totalPage = photos.Count / contentPerPage;
        if (currentPage < totalPage)
        {
            ++currentPage;
            var startPageIndex = currentPage * contentPerPage;
            var endPageIndex = startPageIndex + contentPerPage - 1;
            ShowPhotoFromRange(startPageIndex, endPageIndex);
        }


    }

    private void ShowPhotoFromRange(int startPageIndex, int endPageIndex)
    {
        for (int i = 0; i < photos.Count; i++)
        {
            if (i >= startPageIndex && i <= endPageIndex)
            {
                photos[i].gameObject.SetActive(true);
            }
            else
            {
                photos[i].gameObject.SetActive(false);
            }
        }
    }

    [Button]
    public void PreviousPage()
    {
        float totalPage = photos.Count / contentPerPage;
        if (currentPage > 0)
        {
            --currentPage;

            var startPageIndex = currentPage * contentPerPage;
            var endPageIndex = startPageIndex + contentPerPage - 1;
            ShowPhotoFromRange(startPageIndex, endPageIndex);
        }
    }
}
