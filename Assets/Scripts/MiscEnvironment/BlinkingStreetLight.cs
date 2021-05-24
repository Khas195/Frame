using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
[ExecuteInEditMode]
public class BlinkingStreetLight : MonoBehaviour
{
    [SerializeField]
    AudioSource source = null;
    [SerializeField]
    SpriteRenderer leftLight;
    [SerializeField]
    SpriteRenderer rightLight;
    [SerializeField]
    [Range(0.0f, 1.0f)]
    float leftLightIntensity;

    [SerializeField]
    [Range(0.0f, 1.0f)]
    float rightLightIntensity;

    [SerializeField]

    [Expandable]
    LightFlickringPatter leftPattern = null;
    [SerializeField]
    [Expandable]
    LightFlickringPatter rightPattern = null;

    [SerializeField]
    [ReadOnly]
    float leftCurtime;
    [SerializeField]
    [ReadOnly]
    float rightCurTime;


    private void Start()
    {
        if (leftPattern != null || rightPattern != null)
        {
            source.Play();
        }
        else
        {
            source.Stop();
        }
    }

    private void Update()
    {
        if (leftPattern != null)
        {
            leftCurtime += Time.deltaTime;
            if (leftCurtime >= leftPattern.pattern.GetAnimationCurveTotalTime())
            {
                leftCurtime = 0;
            }
            leftLightIntensity = leftPattern.pattern.Evaluate(leftCurtime);
        }
        if (rightPattern != null)
        {
            rightCurTime += Time.deltaTime;
            if (rightCurTime >= rightPattern.pattern.GetAnimationCurveTotalTime())
            {
                rightCurTime = 0;
            }
            rightLightIntensity = rightPattern.pattern.Evaluate(rightCurTime);
        }
        UpdateLightIntensity();
    }

    private void UpdateLightIntensity()
    {
        var leftColor = leftLight.color;
        leftColor.a = leftLightIntensity;
        leftLight.color = leftColor;

        var rightColor = rightLight.color;
        rightColor.a = rightLightIntensity;
        rightLight.color = rightColor;
    }
}
