using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class AccuracyText : MonoBehaviour
{
    [FormerlySerializedAs("Accuracy")] [FormerlySerializedAs("ScoreText")] 
    [SerializeField] public Text thisAccuracy;

    private static float _accuracy;
    public static int MaxSum;
    public static int CurrentSum;
    public static bool IsEnd;
    
    void Start()
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
            var itog = float.Parse(thisAccuracy.text.Substring(0,thisAccuracy.text.Length - 1));
            Fight.CriticalChance = itog;
            switch (itog)
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
