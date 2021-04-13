using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

[ExecuteInEditMode]
public class ImageEffectTransition : MonoBehaviour
{
    [SerializeField]
    Material transitionMat;
    [SerializeField]
    bool doFlash;
    [SerializeField]
    AnimationCurve flashCurve;
    [SerializeField]
    bool doTransition;
    [SerializeField]
    AnimationCurve transitionCurve;

    [SerializeField]
    [Range(-1, 1)]
    float cutOff;
    [SerializeField]
    [Range(0, 1)]
    float fade;
    [SerializeField]
    float effectTime = 1.0f;
    float curTime = 100.0f;
    private void Start()
    {
        SetFlashStartState();
    }
    private void Update()
    {
        transitionMat.SetFloat("_Cutoff", cutOff);
        transitionMat.SetFloat("_Fade", fade);

        if (curTime <= effectTime)
        {
            if (doTransition)
            {
                cutOff = Mathf.Clamp(transitionCurve.Evaluate(curTime), -1, 1);

            }
            if (doFlash)
            {
                fade = Mathf.Clamp(flashCurve.Evaluate(curTime), 0, 1);
            }
            curTime += Time.deltaTime;
        }
    }
    [Button]
    public void StartEffect()
    {
        SetFlashStartState();
        curTime = 0.0f;
    }
    [Button]
    public void SetFlashStartState()
    {
        if (doTransition)
        {
            cutOff = -1;
        }
        if (doFlash)
        {
            fade = 0;
        }
    }
    [Button]
    public void SetFlashEndState()
    {
        if (doTransition)
        {
            cutOff = 1;
        }
        if (doFlash)
        {
            fade = 0;
        }
    }
    private void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        Graphics.Blit(src, dest, transitionMat);
    }
    private void OnPostRender()
    {

    }
}
