using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CheckInYellow : MonoBehaviour
{
    public int _currentSum;
    public int _maxSum;

    public Text _accuracyText;

    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Z") || other.CompareTag("X") || other.CompareTag("C"))
            if (Input.GetButtonDown(other.tag + "Key"))
            {
                Accuracy.currentSum += 2;
                Accuracy.accuracy = (float)Accuracy.currentSum / Accuracy.maxSum * 100;
                _accuracyText.text = Math.Round(Accuracy.accuracy, 2) + "%";
            }
    }
}