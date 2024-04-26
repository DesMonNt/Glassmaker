using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class PlaterController : MonoBehaviour
{
    public float speed;
    private float moveXInput;
    private float moveYInput;

    public AudioClip clip;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        moveXInput = Input.GetAxis("Horizontal");
        moveYInput = Input.GetAxis("Vertical");
        rb.velocity = new Vector2(moveXInput * speed, moveYInput * speed);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("FIIT"))
            SceneManager.LoadScene("FightingScene");
        if (other.CompareTag("BigB"))
            GetComponent<AudioSource>().PlayOneShot(clip);
    }
}
