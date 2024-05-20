using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    [SerializeField] private GameObject objectToDestroy;
    public void Destroy() => Destroy(objectToDestroy);
}
