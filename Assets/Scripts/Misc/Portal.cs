using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] GameObject coordinatesObject;
     void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            var player = GameObject.FindGameObjectWithTag("Player").transform;
            var camera = GameObject.FindGameObjectWithTag("MainCamera").transform;
            player.position = coordinatesObject.transform.position;
            //camera.position = new Vector3(x,y,z);
        }
    }
}
