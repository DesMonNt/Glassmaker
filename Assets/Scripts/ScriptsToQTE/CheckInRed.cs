using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace ScriptsToQTE
{
    public class CheckInRed : MonoBehaviour
    {
        [FormerlySerializedAs("_accuracyText")] public Text accuracyText;

        public void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Z") || other.CompareTag("X") || other.CompareTag("C"))
                Accuracy.MaxSum += 3;
        }

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
}
