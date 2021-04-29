using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PhotoHolder : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IObserver
{

    [SerializeField]
    private PhotoInfo info;

    [SerializeField]
    Image photoImage = null;
    [SerializeField]
    Image PhotoFrame = null;

    [SerializeField]
    Text influenceText;

    [SerializeField]
    float hoverScale;
    [SerializeField]
    float normalScale;
    [SerializeField]
    AnimationCurve scaleCurve;
    [SerializeField]
    float transitionTime = 0.5f;
    [SerializeField]
    float curTime = -1;
    [SerializeField]
    bool IsInTransition = false;
    [SerializeField]
    bool transitionIn = true;
    [SerializeField]
    Canvas mCanvas;


    private void Awake()
    {
        PostOffice.Subscribes(this, SwitchGameStats.SWITCH_GAME_STATS_EVENT);
    }
    private void Start()
    {

        this.influenceText.gameObject.SetActive(SwitchGameStats.STATS_ON);
        curTime = transitionTime;
    }
    private void OnDestroy()
    {
        if (info != null)
        {
            info.ClearInfo();
        }
        PostOffice.Unsubscribes(this, SwitchGameStats.SWITCH_GAME_STATS_EVENT);
    }
    private void Update()
    {
        if (IsInTransition)
        {
            if (transitionIn)
            {
                if (curTime >= 0)
                {
                    curTime -= Time.deltaTime;
                    var curTargetScale = scaleCurve.Evaluate(curTime);
                    this.transform.localScale = new Vector3(curTargetScale, curTargetScale, 1.0f);
                }
                else
                {
                    IsInTransition = false;
                }
            }
            else
            {
                if (curTime <= transitionTime)
                {
                    curTime += Time.deltaTime;
                    var curTargetScale = scaleCurve.Evaluate(curTime);
                    this.transform.localScale = new Vector3(curTargetScale, curTargetScale, 1.0f);
                }
                else
                {
                    IsInTransition = false;
                }

            }
        }
    }
    public PhotoInfo GetPhotoInfo()
    {
        return info;
    }

    public void SetPhotoInfo(PhotoInfo photoInfo)
    {
        this.info = photoInfo;
        SetImageSprite(this.info.sprite);
        UpdatePhotoInfoInfluence();
    }

    public void UpdatePhotoInfoInfluence()
    {
        Color communistColor = PublicSwayMechanic.GetInstance().GetColorToFaction(ScenarioActor.ActorFaction.Communist);
        Color capitalistColor = PublicSwayMechanic.GetInstance().GetColorToFaction(ScenarioActor.ActorFaction.Capitalist);
        string commieInfluence = ConvertInfluenceToString(this.info.CommunistInfluence);
        string capitalistInfluence = ConvertInfluenceToString(this.info.CapitalistInfluence);
        influenceText.text = commieInfluence.Colorize(communistColor) + " " + capitalistInfluence.Colorize(capitalistColor);
    }

    public static string ConvertInfluenceToString(float influenceValue)
    {
        string influence;
        if (influenceValue >= 0)
        {
            influence = "+ " + influenceValue.ToString();
        }
        else
        {
            influence = "- " + (influenceValue * -1).ToString();
        }

        return influence;
    }

    public void SetImageSprite(Sprite newSprite)
    {
        photoImage.sprite = newSprite;
    }

    public Image GetImage()
    {
        return photoImage;
    }

    [Button]
    private void OnMouseOver()
    {
        IsInTransition = true;
        transitionIn = true;
        if (curTime > transitionTime)
        {
            curTime = transitionTime;
        }
    }
    [Button]
    private void OnMouseExit()
    {
        IsInTransition = true;
        transitionIn = false;
        if (curTime < 0)
        {
            curTime = 0;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        this.OnMouseExit();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        this.OnMouseOver();
    }

    Vector3 originPos = Vector3.zero;

    public void OnBeginDrag()
    {
        LogHelper.Log("Begin Drag Photo");
        originPos = this.transform.position;
        this.OnMouseExit();
        NewsPaperPanel.GetInstance().SetCurrentSelection(this);
        this.PhotoFrame.raycastTarget = false;
        this.photoImage.raycastTarget = false;
    }
    public void OnDrag()
    {
        this.transform.position = Input.mousePosition;
    }
    public void OnEndDrag()
    {
        this.photoImage.raycastTarget = true;
        this.PhotoFrame.raycastTarget = true;
        LogHelper.Log("Dropping Photo");
        this.transform.position = originPos;
        NewsPaperPanel.GetInstance().OnPhotoDrop.Invoke();
        NewsPaperPanel.GetInstance().SetCurrentSelection(null);
    }

    public void ReceiveData(DataPack pack, string eventName)
    {
        if (eventName == SwitchGameStats.SWITCH_GAME_STATS_EVENT)
        {
            this.influenceText.gameObject.SetActive(SwitchGameStats.STATS_ON);
        }
    }

}
