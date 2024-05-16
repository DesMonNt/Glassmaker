using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ViewDescription : MonoBehaviour
{
    public bool IsAttack;
    public bool IsSkill;
    public bool IsUltimate;
    
    [FormerlySerializedAs("Description")] [SerializeField] private Image description;

    private void Awake()
    {
        IsAttack = false;
        IsSkill = false;
        IsUltimate = false;
        description.GameObject().SetActive(false);
    }

    private void OnMouseOver() => description.GameObject().SetActive(true);

    private void OnMouseExit() => description.GameObject().SetActive(false);

    private void OnMouseDown()
    {
        if (this.GameObject().name is "Attack")
            IsAttack = true;
        else if (this.GameObject().name is "Skill")
            IsSkill = true;
        else if (this.GameObject().name is "Ultimate")
            IsUltimate = true;
    }
}
