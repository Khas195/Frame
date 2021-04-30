using NaughtyAttributes;
using UnityEngine;

[CreateAssetMenu(fileName = "SwayData", menuName = "Data/SwayData", order = 1)]
public class PublicSwayData : ScriptableObject
{

    [SerializeField]
    [MinMaxSlider(0, 100)]
    [OnValueChanged("RoundUpSwayPercentage")]
    [InfoBox("LHS = Communist, Middle = Neutral, RHS = Capitalist")]
    public Vector2 swayPercentage;

    [SerializeField]
    [InfoBox("LHS = Communist, Middle = Neutral, RHS = Capitalist")]
    [MinMaxSlider(0, 200)]
    [OnValueChanged("OnInFluenceChanged")]
    public Vector2 influence;


}
