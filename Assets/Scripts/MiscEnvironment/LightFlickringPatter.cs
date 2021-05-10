using UnityEngine;

[CreateAssetMenu(fileName = "FlickeringData", menuName = "Data/LightFlicker", order = 1)]
public class LightFlickringPatter : ScriptableObject
{
    public AnimationCurve pattern = null;
}
