using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerShards : MonoBehaviour
{
    [SerializeField] private GameObject canvas;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Instantiate(canvas, new Vector3(0,0,0), new Quaternion());
        }
    }
}
