using System;
using UnityEngine;
using UnityEngine.Serialization;

public class CheckCollision : MonoBehaviour
{
    private int _currentSum;
    private int _maxSum;

    [FormerlySerializedAs("_isUpdateMax")] [SerializeField] private bool isUpdateMax;

    private void Update()
    {
        if (!Input.GetKeyDown((KeyCode)Enum.Parse(typeof(KeyCode), tag))) 
            return;
        if (!IsInRedZone()) 
            return;
        if (IsInGreenZone())
            AccuracyText.CurrentSum += 3;
        else if (IsInYellowZone())
            AccuracyText.CurrentSum += 2;
        else 
            AccuracyText.CurrentSum ++;
    }

    private bool IsInGreenZone() =>
        transform.position.x is <= 36 and >= -36 && transform.position.y is <= 41 and >= -12;
        
    private bool IsInYellowZone() =>
        transform.position.x is <= 50 and >= -50 && transform.position.y is <= 64 and >= -41;

    private bool IsInRedZone() =>
        transform.position.x is <= 77 and >= -77 && transform.position.y is <= 88 and >= -73;
}
