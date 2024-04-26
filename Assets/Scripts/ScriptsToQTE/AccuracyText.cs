using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class AccuracyText : MonoBehaviour
{
    [FormerlySerializedAs("ScoreText")] [SerializeField] Text Accuracy;

    public static float accuracy;
    public static int maxSum;
    public static int currentSum;
    
    void Start()
    {
        maxSum = 0;
        currentSum = 0;
        Accuracy.text = "100.00%";
    }

    public void Update()
    {
        if (maxSum == 0)
            return;
        accuracy = (float)currentSum / maxSum * 100;
        Accuracy.text = Math.Round(accuracy, 2) + "%";
    }
}
