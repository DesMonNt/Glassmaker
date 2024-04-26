using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CheckInGreenZone : MonoBehaviour
{
    public int _currentSum;
    public int _maxSum;

    public Text _accuracyText;

    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Z") || other.CompareTag("X") || other.CompareTag("C"))
        {
            if (Input.GetButtonDown(other.tag + "Key"))
            {
                Accuracy.currentSum += 3;
                Accuracy.accuracy = (float)Accuracy.currentSum / Accuracy.maxSum * 100;
                _accuracyText.text = Math.Round(Accuracy.accuracy, 2) + "%";
            }

            else
            {
                Accuracy.accuracy = (float)Accuracy.currentSum / Accuracy.maxSum * 100;
                _accuracyText.text = Math.Round(Accuracy.accuracy, 2) + "%";
            }
        }
    }
}
