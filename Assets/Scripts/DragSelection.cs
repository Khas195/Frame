using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DragSelection : SingletonMonobehavior<DragSelection>
{
    [SerializeField]
    Camera playerCamera = null;
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


    Vector2 startPos = Vector2.zero;
    bool selectionMode = false;
    bool inPhotoMode = false;
    private void Start()
    {
        cameraFrame.gameObject.SetActive(false);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            if (selectionMode == false)
            {
                EnterCameraMode();
            }
            else
            {
                ExitCameraMode();
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (inPhotoMode)
            {
                ExitPhotoMode();
            }
        }
    }

    private void EnterCameraMode()
    {
        OnEnterCameraMode.Invoke();
        selectionMode = true;
        cameraFrame.gameObject.SetActive(true);
    }

    public bool IsInSelectionMode()
    {
        return selectionMode;
    }

    public void ExitCameraMode()
    {
        OnExitCameraMode.Invoke();
        selectionMode = false;
        cameraFrame.gameObject.SetActive(false);
    }
    public void ExitPhotoMode()
    {
        var color = photoImage.color;
        color.a = 0;
        photoImage.color = color;
        photoFrame.gameObject.SetActive(false);
        inPhotoMode = false;
    }
    public void EnterPhotoMode()
    {
        inPhotoMode = true;
        var color = photoImage.color;
        color.a = 1;
        photoImage.color = color;
        photoFrame.gameObject.SetActive(true);
        if (selectionMode)
        {
            ExitCameraMode();
        }
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
