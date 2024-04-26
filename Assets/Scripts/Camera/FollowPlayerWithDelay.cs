using UnityEngine;

namespace Camera
{
    public class CameraFollow2D : MonoBehaviour
    {

        public float damping = 3f;
        public Vector2 offset = new(2f, 1f);
        public bool faceLeft;
        private Transform _player;
        private int _lastX;

        private void Start()
        {
            offset = new Vector2(Mathf.Abs(offset.x), offset.y);
            FindPlayer(faceLeft);
        }

        private void FindPlayer(bool playerFaceLeft)
        {
            _player = GameObject.FindGameObjectWithTag("Player").transform;
            _lastX = Mathf.RoundToInt(_player.position.x);
            transform.position = playerFaceLeft ? new Vector3(_player.position.x - offset.x, _player.position.y + offset.y, transform.position.z) : new Vector3(_player.position.x + offset.x, _player.position.y + offset.y, transform.position.z);
        }

        private void Update()
        {
            if (!_player) return;
            var currentX = Mathf.RoundToInt(_player.position.x);
            if (currentX > _lastX) faceLeft = false; else if (currentX < _lastX) faceLeft = true;
            _lastX = Mathf.RoundToInt(_player.position.x);

            var target = faceLeft ? new Vector3(_player.position.x - offset.x, _player.position.y + offset.y, transform.position.z) : new Vector3(_player.position.x + offset.x, _player.position.y + offset.y, transform.position.z);
            var currentPosition = Vector3.Lerp(transform.position, target, damping * Time.deltaTime);
            transform.position = currentPosition;
        }
    }
}