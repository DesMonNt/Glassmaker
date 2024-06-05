using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace ScriptsToQTE
{
    public class CheckInGreenZone : MonoBehaviour
    {
        [FormerlySerializedAs("_accuracyText")] public Text accuracyText;

        public void OnTriggerStay2D(Collider2D other)
        {
            if (!other.CompareTag("Z") && !other.CompareTag("X") && !other.CompareTag("C")) 
                return;
            if (Input.GetButtonDown(other.tag + "Key"))
            {
                Accuracy.CurrentSum += 3;
                Accuracy.accuracy = (float)Accuracy.CurrentSum / Accuracy.MaxSum * 100;
                accuracyText.text = Math.Round(Accuracy.accuracy, 2) + "%";
            }

            else
            {
                Accuracy.accuracy = (float)Accuracy.CurrentSum / Accuracy.MaxSum * 100;
                accuracyText.text = Math.Round(Accuracy.accuracy, 2) + "%";
            }
        }
    }
}
