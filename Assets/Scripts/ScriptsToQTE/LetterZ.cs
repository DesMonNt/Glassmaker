using UnityEngine;

namespace ScriptsToQTE
{
    public class LetterZ : MonoBehaviour
    {
        private Rigidbody2D _rb;

        private void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        public int speed;
        public bool isLeft = true;

        private void Update()
        {
            if (isLeft)
            {
                transform.position = new Vector3(-240, 14.5f);
                isLeft = false;
                return;
            }
                
            transform.position += new Vector3(0.4f * speed * Time.deltaTime, 0);
            if (transform.position.x >= 0) isLeft = true;

        }
    }
}
