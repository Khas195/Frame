using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragSelection : MonoBehaviour
{
    [SerializeField]
    Camera playerCamera = null;
    [SerializeField]
    Canvas mCanvas = null;
    [SerializeField]
    RectTransform cameraFrame;
    [SerializeField]
    Image photoImage = null;


    Vector2 startPos = Vector2.zero;
    bool selectionMode = false;
    private void Start()
    {
        cameraFrame.gameObject.SetActive(false);
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            selectionMode = true;
            cameraFrame.gameObject.SetActive(true);
        }
        if (selectionMode)
        {
            if (Input.GetMouseButton(0))
            {
                cameraFrame.position = GetMousPosOnCanvas();
            }
            else if (Input.GetMouseButtonUp(0))
            {
                ExitSelectionMode();
            }
        }
    }

    public void ExitSelectionMode()
    {
        selectionMode = false;
        cameraFrame.gameObject.SetActive(false);
        HideImage();
    }
    public void HideImage()
    {
        var color = photoImage.color;
        color.a = 0;
        photoImage.color = color;

    }
    public void ViewImage()
    {
        var color = photoImage.color;
        color.a = 1;
        photoImage.color = color;
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
