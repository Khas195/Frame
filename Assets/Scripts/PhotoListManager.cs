using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PhotoListManager : MonoBehaviour, IObserver
{
    public const string DISCARD_PHOTO_EVENT = "DISCARD_PHOTO_EVENT";
    [SerializeField]
    AudioSource discardSource;
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
    void Start()
    {
        PostOffice.Subscribes(this, DISCARD_PHOTO_EVENT);
    }
    private void OnDestroy()
    {
        PostOffice.Unsubscribes(this, DISCARD_PHOTO_EVENT);
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
    private void Discard(PhotoHolder photo)
    {
        photos.Remove(photo);
        Destroy(photo.gameObject);
    }
    public void DiscardButton(PhotoHolder holder)
    {
        var package = DataPool.GetInstance().RequestInstance();
        var photosToDiscard = new List<PhotoInfo>();
        photosToDiscard.Add(holder.GetPhotoInfo());
        package.SetValue("PhotoInfos", photosToDiscard);
        PostOffice.SendData(package, PhotoListManager.DISCARD_PHOTO_EVENT);
        DataPool.GetInstance().ReturnInstance(package);
    }
    public void ReceiveData(DataPack pack, string eventName)
    {
        if (eventName == DISCARD_PHOTO_EVENT)
        {
            bool discardedStuff = false;
            var infos = pack.GetValue<List<PhotoInfo>>("PhotoInfos");
            if (infos != null && infos.Count > 0)
            {
                for (int i = 0; i < infos.Count; i++)
                {
                    var holder = photos.Find(x => x.GetPhotoInfo() == infos[i]);
                    if (holder)
                    {
                        Discard(holder);
                        discardedStuff = true;
                    }
                }

            }
            if (discardedStuff && discardSource)
            {
                discardSource.Play();
            }
        }
    }
}
