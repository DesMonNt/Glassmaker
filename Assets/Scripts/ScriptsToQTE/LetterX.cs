using UnityEngine;

namespace ScriptsToQTE
{
    public class LetterX : MonoBehaviour
    {
        private Rigidbody2D _rb;

        private void Start() => _rb = GetComponent<Rigidbody2D>();

        public int speed;
        public bool isDown = true;

        private void Update()
        {
            if (isDown)
            {
                transform.position = new Vector3(0, 380f);
                isDown = false;
                return;
            }
            
            transform.position += new Vector3(0, -0.4f * speed * Time.deltaTime);
            if (transform.position.y <= 0) 
                isDown = true;
        }
    }
}
