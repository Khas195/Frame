using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DragSelection : SingletonMonobehavior<DragSelection>
{
    [SerializeField]
    Canvas mCanvas = null;
    [SerializeField]
    RectTransform cameraFrame;
    [SerializeField]
    RectTransform photoFrame;
    [SerializeField]
    Image photoImage = null;
    [SerializeField]
    UnityEvent OnEnterCameraMode = new UnityEvent();
    [SerializeField]
    UnityEvent OnExitCameraMode = new UnityEvent();

    private void Start()
    {
        cameraFrame.gameObject.SetActive(false);
    }

    public void EnterCameraMode()
    {
        OnEnterCameraMode.Invoke();
        cameraFrame.gameObject.SetActive(true);
    }


    public void ExitCameraMode()
    {
        OnExitCameraMode.Invoke();
        cameraFrame.gameObject.SetActive(false);
    }
    public void ExitPhotoMode()
    {
        var color = photoImage.color;
        color.a = 0;
        photoImage.color = color;
        photoFrame.gameObject.SetActive(false);
    }
    public void EnterPhotoMode()
    {
        var color = photoImage.color;
        color.a = 1;
        photoImage.color = color;
        photoFrame.gameObject.SetActive(true);
    }
    private Vector2 GetMousPosOnCanvas()
    {
        Vector2 uiPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(mCanvas.transform as RectTransform, Input.mousePosition, mCanvas.worldCamera, out uiPos);
        return mCanvas.transform.TransformPoint(uiPos);
    }
    public Vector2 GetPositionOnCanvas(RectTransform targetUI)
    {
        return mCanvas.transform.TransformPoint(targetUI.position);
    }
}
