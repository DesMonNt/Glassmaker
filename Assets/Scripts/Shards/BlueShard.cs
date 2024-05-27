using System;
using System.Collections;
using System.Collections.Generic;
using FightingScene;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class AzureShard : MonoBehaviour
{
    [SerializeField] public Text nameOfBuff;

    public AzureShard(string name) => nameOfBuff.text = name;

    public GameObject canvas;

    public void Start()
    {
        this.GameObject().SetActive(false);
    }

    public void GetClicked()
    {
        canvas.SetActive(false);
        Debug.Log($"{nameOfBuff.text}");
        SetedUnitsFromPreviousScene.SaveShard(BuffInfo.KeyToAzureBuff[nameOfBuff.text]);
    }
}
