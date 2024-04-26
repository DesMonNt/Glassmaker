using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class FighterTurnMeter : MonoBehaviour
{
    [FormerlySerializedAs("StepValue")] [SerializeField] private float stepValue;
    [FormerlySerializedAs("MaxValue")] [SerializeField] private float maxValue;

    private float _value;

    public bool CanOffensive => _value >= maxValue;

    public void Increase() => _value = Mathf.Clamp(_value + stepValue, 0, maxValue);

    public void Reset() => _value = 0;
}
