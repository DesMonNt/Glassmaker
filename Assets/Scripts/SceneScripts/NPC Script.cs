using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCScript : MonoBehaviour
{
    [SerializeField] public Text dialoge;

    private void Start() => dialoge.gameObject.SetActive(false);

    private void OnTriggerStay2D(Collider2D other)
    {
        dialoge.text = "Welcome to the club, buddy...";
        if (other.CompareTag("Player"))
            dialoge.gameObject.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D other) => dialoge.gameObject.SetActive(false);
}
