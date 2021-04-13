using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

public class PhotoListManager : SingletonMonobehavior<PhotoListManager>
{
    [SerializeField]
    int contentPerPage;
    [SerializeField]
    int currentPage = 0;
    [SerializeField]
    List<Image> photos;
    [SerializeField]
    Transform contentRoot;
    [SerializeField]
    GameObject photoExample;
    [SerializeField]
    GameObject photoListRoot = null;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            photoListRoot.SetActive(!photoListRoot.gameObject.activeSelf);
        }
    }

    public void AddPhoto(Sprite imageSprite)
    {
        var newGameObject = GameObject.Instantiate(photoExample, contentRoot);
        var photoHolder = newGameObject.GetComponent<PhotoHolder>();
        photoHolder.SetImageSprte(imageSprite);
        photos.Add(photoHolder.GetImage());
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
