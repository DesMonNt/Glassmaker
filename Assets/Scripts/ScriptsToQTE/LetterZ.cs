using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LetterZ : MonoBehaviour
{
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public int speed;
    public bool isLeft = true;
    
    void Update()
    {
        // float horizontalInput = Input.GetAxis("Horizontal");
        // float verticalInput = Input.GetAxis("Vertical");

        // transform.position = transform.position + new Vector3(horizontalInput * speed * Time.deltaTime, verticalInput * speed * Time.deltaTime);

        if (isLeft)
        {
            transform.position = new Vector3(-240, 14.5f);
            isLeft = false;
        }

        if (!isLeft)
        {
            transform.position = transform.position + new Vector3(0.4f * speed * Time.deltaTime, 0);
            if (transform.position.x >= 0)
            {
                isLeft = true;
            }
                
    
        }
    
    }
}
