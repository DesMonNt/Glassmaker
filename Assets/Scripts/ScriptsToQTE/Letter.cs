using UnityEngine;
using UnityEngine.Serialization;

namespace ScriptsToQTE
{
    public class Letter : MonoBehaviour
    {
        [FormerlySerializedAs("_speed")] public float speed;
        private bool _inCenter;

        private Vector3 _targetPosition;
        private Transform _transform;

        private Vector3 _direction;

        private void Start()
        {
            _transform = transform;
            _targetPosition = new Vector3(0f, 1f);
            _direction = (_targetPosition - _transform.position).normalized;
        
        }

        private void Update() => GoToTarget();

        private void GoToTarget()
        {
            if (!RandomSpawner.CanSpawn)
                Destroy(gameObject);
            _transform.position += speed * Time.deltaTime * _direction;
        }
    }
}
