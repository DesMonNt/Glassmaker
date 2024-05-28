using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Canvaser : MonoBehaviour
{
    public Sprite sprite;

    public void Start()
    {
        var png = GetComponent<Image>();
        png.sprite = sprite;
    }
}