using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class BlurEffect : MonoBehaviour
{
    [SerializeField]
    Material blurMat;
    private void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        if (blurMat != null)
        {
            Graphics.Blit(src, dest, blurMat);
        }
        else
        {
            Graphics.Blit(src, dest);
        }
    }
}
