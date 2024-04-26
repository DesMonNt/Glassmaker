using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LetterC : MonoBehaviour
{
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public int speed;
    public bool isRight = true;
    void Update()
    {
        // float horizontalInput = Input.GetAxis("Horizontal");
        // float verticalInput = Input.GetAxis("Vertical");

        // transform.position = transform.position + new Vector3(horizontalInput * speed * Time.deltaTime, verticalInput * speed * Time.deltaTime);

        if (isRight)
        {
            transform.position = new Vector3(570, 14f);
            isRight = false;
        }

        if (!isRight)
        {
            transform.position = transform.position + new Vector3(-0.4f * speed * Time.deltaTime, 0);
            if (transform.position.x <= 0)
            {
                isRight = true;
            }
                
    
        }
    
    }
}
