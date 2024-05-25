using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class PlaterController : MonoBehaviour
{
    public float speed;
    private float moveXInput;
    private float moveYInput;

    [SerializeField] public GameObject loadingScene;
    private LoadingBar _loading;
    [SerializeField] public string nameNextScene;

    private Rigidbody2D rb;

    private void Start()
    {
        loadingScene.SetActive(false);
        _loading = loadingScene.GetComponent<LoadingBar>();
        //_loading.nameOfScene = nameNextScene;
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
        {
            loadingScene.GetComponent<LoadingBar>().loadingBar.color = new Color(255, 0, 0, 255);
            loadingScene.SetActive(true);
            loadingScene.GetComponent<LoadingBar>().loadingBar.color = new Color(0, 255, 0, 255);
        }
            
    }
}
