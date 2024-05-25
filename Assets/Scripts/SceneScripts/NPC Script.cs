using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class NPCScript : MonoBehaviour
{
    public GameObject dialoge;
    [SerializeField] public Sprite dialoge1;
    [SerializeField] public Sprite dialoge2;
    private bool _isStart;
    public GameObject blackOut;

    private SpriteRenderer _renderer;

    private void Start()
    {
        _renderer = dialoge.GetComponent<SpriteRenderer>();
        dialoge.SetActive(false);
        blackOut.SetActive(false);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!other.CompareTag("Player") || _isStart) 
            return;
        _isStart = true;
        StartCoroutine(GetDialoge());
    }

    private IEnumerator GetDialoge()
    {
        _renderer.sprite = dialoge1;
        dialoge.SetActive(true);
        blackOut.SetActive(true);
        yield return new WaitForSeconds(8);
        _renderer.sprite = dialoge2;
        yield return new WaitForSeconds(8);
        dialoge.SetActive(false);
        blackOut.SetActive(false);
    }
}
