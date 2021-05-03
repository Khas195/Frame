using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PhotoListManager : MonoBehaviour, IObserver
{
    [SerializeField]
    AudioSource discardSource;
    [SerializeField]
    List<PhotoHolder> photos;
    [SerializeField]
    Transform contentRoot;
    [SerializeField]
    float photoPileZoneRadius = 1.0f;



    [SerializeField]
    GameObject photoExample;
    [SerializeField]
    GameObject photoListRoot = null;


    void Awake()
    {
        PostOffice.Subscribes(this, GameEvent.PhotoEvent.DISCARD_PHOTO_EVENT);
        PostOffice.Subscribes(this, GameEvent.NewspaperEvent.NEWSPAPER_PUBLISHED_EVENT);
    }
    private void OnDestroy()
    {
        PostOffice.Unsubscribes(this, GameEvent.PhotoEvent.DISCARD_PHOTO_EVENT);
        PostOffice.Unsubscribes(this, GameEvent.NewspaperEvent.NEWSPAPER_PUBLISHED_EVENT);
    }
    public void Hide()
    {
        photoListRoot.SetActive(false);
    }
    public void Show()
    {
        photoListRoot.SetActive(true);
    }

    public void AddPhoto(PhotoInfo photoInfo)
    {
        var newGameObject = GameObject.Instantiate(photoExample, contentRoot);
        var photoHolder = newGameObject.GetComponent<PhotoHolder>();
        photoHolder.SetPhotoInfo(photoInfo);
        photos.Add(photoHolder);
        var contentPos = contentRoot.GetComponent<RectTransform>().position;
        var randomPos = UnityEngine.Random.insideUnitSphere * photoPileZoneRadius + contentPos;
        photoHolder.GetComponent<RectTransform>().position = randomPos;
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
        package.SetValue(GameEvent.PhotoEvent.DiscardPhotoEventData.PHOTO_INFOS, photosToDiscard);
        PostOffice.SendData(package, GameEvent.PhotoEvent.DISCARD_PHOTO_EVENT);
        DataPool.GetInstance().ReturnInstance(package);
    }
    public void ReceiveData(DataPack pack, string eventName)
    {
        if (eventName == GameEvent.PhotoEvent.DISCARD_PHOTO_EVENT)
        {
            HandlePhotosDiscardEvent(pack);
        }
        else if (eventName == GameEvent.NewspaperEvent.NEWSPAPER_PUBLISHED_EVENT)
        {
            for (int i = 0; i < this.photos.Count; i++)
            {
                this.photos[i].UpdatePhotoInfoInfluence();
            }
        }
    }

    private void HandlePhotosDiscardEvent(DataPack pack)
    {
        bool discardedStuff = false;
        var infos = pack.GetValue<List<PhotoInfo>>(GameEvent.PhotoEvent.DiscardPhotoEventData.PHOTO_INFOS);
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
