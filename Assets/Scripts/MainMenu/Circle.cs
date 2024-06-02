using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Circle : MonoBehaviour
{
    public Vector3 startPos;
    [FormerlySerializedAs("EndPos")] public Vector3 endPos;
    public Vector3 CurrentPos => transform.position;

    public float time;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        endPos = CurrentPos;
    }

    // Update is called once per frame
    void Update()
    {
        if (time < 1)
        {
            transform.position = Vector3.Lerp(startPos, endPos, Sigmoid(time));
            time += Time.deltaTime * 2;
        }
    }

    private float Sigmoid(float x) => 2 / (1 + Mathf.Exp(-(x) * 3)) - .5f;
}
