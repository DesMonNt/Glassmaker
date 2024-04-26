using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public static class Accuracy
{
    public static float accuracy;
    public static int maxSum;
    public static int currentSum;

    public static Text accuracyText;

    public static void Update()
    {
        accuracyText.text = Math.Round(accuracy, 2) + "%";
    }
}
