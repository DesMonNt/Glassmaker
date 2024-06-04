using UnityEngine;

namespace ScriptsToQTE
{
    public class LetterC : MonoBehaviour
    {
        private Rigidbody2D _rb;

        private void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        public int speed;
        public bool isRight = true;

        private void Update()
        {
            if (isRight)
            {
                transform.position = new Vector3(570, 14f);
                isRight = false;
                return;
            }
            transform.position += new Vector3(-0.4f * speed * Time.deltaTime, 0);
            if (transform.position.x <= 0) isRight = true;

        }
    }
}
