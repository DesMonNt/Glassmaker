using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] float x,y,z;
     void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            var player = GameObject.FindGameObjectWithTag("Player").transform;
            var camera = GameObject.FindGameObjectWithTag("MainCamera").transform;
            player.position = new Vector3(x,y,z);
            //camera.position = new Vector3(x,y,z);
        }
    }
}
