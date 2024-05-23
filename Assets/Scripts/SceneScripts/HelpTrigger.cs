using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Trigger : MonoBehaviour
{
    [SerializeField] public GameObject help;

    private void Start() => help.SetActive(false);

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            help.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D other) => help.SetActive(false);
}
