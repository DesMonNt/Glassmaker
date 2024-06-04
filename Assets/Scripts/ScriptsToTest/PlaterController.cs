using GlobalScripts;
using UnityEngine;

namespace ScriptsToTest
{
    public class PlaterController : MonoBehaviour
    {
        public float speed;
        private float _moveXInput;
        private float _moveYInput;

        public GameObject loadingScene;

        private Rigidbody2D _rb;

        private void Start()
        {
            loadingScene.SetActive(false);
            loadingScene.GetComponent<LoadingBar>();
            _rb = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            _moveXInput = Input.GetAxis("Horizontal");
            _moveYInput = Input.GetAxis("Vertical");
            _rb.velocity = new Vector2(_moveXInput * speed, _moveYInput * speed);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("FIIT")) return;
            loadingScene.GetComponent<LoadingBar>().loadingBar.color = new Color(255, 0, 0, 255);
            loadingScene.SetActive(true);
            loadingScene.GetComponent<LoadingBar>().loadingBar.color = new Color(0, 255, 0, 255);

        }
    }
}
