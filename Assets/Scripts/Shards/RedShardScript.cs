using System;
using System.Collections;
using System.Collections.Generic;
using Effects;
using FightingScene;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CrimsonShardScript : MonoBehaviour
{
    [SerializeField] public Text nameOfBuff;

    public CrimsonShardScript(string name) => nameOfBuff.text = name;
    
    public GameObject canvas;
    
    public void Start() => this.GameObject().SetActive(false);

    public void GetClicked()
    {
        canvas.SetActive(false);
        SetUnitsFromPreviousScene.SaveShard(BuffInfo.KeyToCrimsonBuff[nameOfBuff.text]);
    }
}
