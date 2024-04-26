using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LetterX : MonoBehaviour
{
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public int speed;
    public bool isDown = true;
    void Update()
    {
        if (isDown)
        {
            transform.position = new Vector3(0, 380f);
            isDown = false;
        }

        if (!isDown)
        {
            transform.position = transform.position + new Vector3(0, -0.4f * speed * Time.deltaTime);
            if (transform.position.y <= 0)
            {
                isDown = true;
            }
        }
    }
}
