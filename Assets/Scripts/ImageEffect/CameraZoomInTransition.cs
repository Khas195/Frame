using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
[ExecuteInEditMode]
public class CameraZoomInTransition : MonoBehaviour
{
    [SerializeField]
    Material blurMaterial;
    [SerializeField]
    TakePicture playerCamera;
    [SerializeField]
    AnimationCurve blurCurve;
    [SerializeField]
    AnimationCurve zoomCurve;
    [SerializeField]
    float effectTime = 1.0f;
    [SerializeField]
    float blurValue = 0.5f;
    [SerializeField]
    [Range(0, 1)]
    float zoomValue = 0.5f;
    [SerializeField]
    float curTime = 100f;

    void Update()
    {
        blurMaterial.SetFloat("_Size", blurValue);
        playerCamera.LerpBetweenSize(zoomValue);
        if (curTime <= effectTime)
        {
            zoomValue = zoomCurve.Evaluate(curTime);
            blurValue = blurCurve.Evaluate(curTime);
            curTime += Time.deltaTime;
        }
        else
        {
            this.enabled = false;
        }
    }
    [Button]
    public void TransitionIn()
    {
        curTime = 0.0f;
        this.enabled = true;
        SetStartState();
    }
    [SerializeField]
    [Button]
    public void SetStartState()
    {
        blurValue = 0;
        zoomValue = 1;
    }
}
