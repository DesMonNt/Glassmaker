using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Trigger : MonoBehaviour
{
    [SerializeField] public GameObject help;
    private bool _isUsed;

    private void Start() => help.SetActive(false);

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player") || _isUsed) 
            return;
        _isUsed = true;
        StartCoroutine(EightSecondsWait());
    }

    private IEnumerator EightSecondsWait()
    {
        help.SetActive(true);
        yield return new WaitForSeconds(6);
        help.SetActive(false);
    } 
}
