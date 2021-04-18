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
    float effectTime = 1.0f;
    [SerializeField]
    float blurValue = 0.5f;
    [SerializeField]
    [Range(0, 1)]
    float zoomValue = 0.5f;
    [SerializeField]
    [ReadOnly]
    float curTime = 100f;
    [SerializeField]
    [ReadOnly]
    bool isInTransition = false;

    void Update()
    {
        blurMaterial.SetFloat("_Size", blurValue);
        playerCamera.LerpBetweenSize(zoomValue);
        if (curTime <= effectTime)
        {
            zoomValue = zoomCurve.Evaluate(curTime);
            blurValue = blurCurve.Evaluate(curTime);
            curTime += Time.deltaTime;
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
        curTime = 0.0f;
        this.enabled = true;
        SetStartState();
        source.Play();
    }
    [SerializeField]
    public void TransitionOut()
    {
        curTime = 100f;
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
