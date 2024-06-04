using System;
using ScriptsToQTE;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class CheckInYellow : MonoBehaviour
{
    [FormerlySerializedAs("_currentSum")] public int currentSum;
    [FormerlySerializedAs("_maxSum")] public int maxSum;

    [FormerlySerializedAs("_accuracyText")] public Text accuracyText;

    public void OnTriggerStay2D(Collider2D other)
    {
        if (!other.CompareTag("Z") && !other.CompareTag("X") && !other.CompareTag("C")) 
            return;
        if (!Input.GetButtonDown(other.tag + "Key")) 
            return;
        Accuracy.CurrentSum += 2;
        Accuracy.accuracy = (float)Accuracy.CurrentSum / Accuracy.MaxSum * 100;
        accuracyText.text = Math.Round(Accuracy.accuracy, 2) + "%";
    }
}