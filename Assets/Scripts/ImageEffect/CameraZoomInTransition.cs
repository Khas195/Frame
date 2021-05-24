using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class CameraZoomInTransition : MonoBehaviour
{
    [SerializeField]
    AudioSource source;
    [SerializeField]
    Text cameraState;
    [SerializeField]
    Text cameraInstruction;
    [SerializeField]
    Material blurMaterial;
    [SerializeField]
    TakePicture playerCamera;
    [SerializeField]
    AnimationCurve blurCurve;
    [SerializeField]
    AnimationCurve zoomCurve;
    [SerializeField]
    float blurValue = 0.5f;
    [SerializeField]
    [Range(0, 1)]
    float zoomValue = 0.5f;
    [SerializeField]
    [ReadOnly]
    float curBlurTime = 100f;
    float curZoomTime = 100f;
    [SerializeField]
    [ReadOnly]
    bool isInTransition = false;

    void Update()
    {
        blurMaterial.SetFloat("_Size", blurValue);
        playerCamera.LerpBetweenSize(zoomValue);
        bool isBluring = false;
        bool isZooming = false;
        if (curBlurTime <= blurCurve.GetAnimationCurveTotalTime())
        {
            blurValue = blurCurve.Evaluate(curBlurTime);
            curBlurTime += Time.deltaTime;
            isBluring = true;
        }
        if (curZoomTime <= zoomCurve.GetAnimationCurveTotalTime())
        {
            zoomValue = zoomCurve.Evaluate(curZoomTime);
            curZoomTime += Time.deltaTime;
            isZooming = true;
        }
        if (isBluring && isZooming)
        {
            cameraState.text = "Focusing...";
            var tempColor = cameraInstruction.color;
            tempColor.a = Mathf.Lerp(1.0f, 0.0f, zoomValue);
            cameraInstruction.color = tempColor;

        }
        else
        {
            this.enabled = false;
            cameraState.text = "Ready ";

        }
    }
    [Button]
    public void TransitionIn()
    {
        curBlurTime = 0.0f;
        curZoomTime = 0.0f;
        this.enabled = true;
        SetStartState();
        source.Play();
    }
    private void OnDisable()
    {
        source.Stop();
    }
    [SerializeField]
    public void TransitionOut()
    {
        curBlurTime = 100f;
        curZoomTime = 100f;
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
