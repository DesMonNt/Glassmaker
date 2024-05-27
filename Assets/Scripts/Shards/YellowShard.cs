using System.Collections;
using System.Collections.Generic;
using FightingScene;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class AmberShard : MonoBehaviour
{
    [SerializeField] public Text nameOfBuff;

    public AmberShard(string name) => nameOfBuff.text = name;
    
    public GameObject canvas;
    
    public void Start()
    {
        this.GameObject().SetActive(false);
    }

    public void GetClicked()
    {
        canvas.SetActive(false);
        Debug.Log($"{nameOfBuff.text}");
        SetedUnitsFromPreviousScene.SaveShard(BuffInfo.KeyToAmberBuff[nameOfBuff.text]);
    }
}
