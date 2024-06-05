using System;
using FightingScene;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace ScriptsToQTE
{
    public class AccuracyText : MonoBehaviour
    {
        [FormerlySerializedAs("Accuracy")] [FormerlySerializedAs("ScoreText")] 
        public Text thisAccuracy;

        private static float _accuracy;
        public static int MaxSum;
        public static int CurrentSum;
        public static bool IsEnd;

        private void Start()
        {
            MaxSum = 0;
            CurrentSum = 0;
            thisAccuracy.text = "100.00%";
        }
    
        public void Update()
        {
            if (MaxSum == 0)
                return;
            _accuracy = (float)CurrentSum / MaxSum * 100;
            if (IsEnd)
            {
                var result = float.Parse(thisAccuracy.text[..^1]);
                Fight.CriticalChance = result;
                switch (result)
                {
                    case >= 50:
                        thisAccuracy.color = new Color(0, 245, 0);
                        IsEnd = false;
                        break;
                    case >= 25 and < 50:
                        thisAccuracy.color = new Color(255, 255, 0);
                        IsEnd = false;
                        break;
                    case < 25:
                        thisAccuracy.color = new Color(245, 0, 0);
                        IsEnd = false;
                        break;
                }
            }
            thisAccuracy.text = Mathf.Clamp((float)(2 * Math.Round(_accuracy, 2)), 0, 100) + "%";
        }
    }
}
