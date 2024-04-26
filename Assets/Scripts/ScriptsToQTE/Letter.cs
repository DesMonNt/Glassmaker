using UnityEngine;
using UnityEngine.Serialization;

public class Letter : MonoBehaviour
{
    [FormerlySerializedAs("_speed")] public float speed;
    private bool _inCenter;

    private Vector3 _targetPosition;
    private Transform _transform;

    void Start()
    {
        _transform = transform;
        _targetPosition = new Vector3(0f, 1f);
    }

    void Update() => GoToTarget();

    private void GoToTarget()
    {
        var target = (_targetPosition - _transform.position).normalized;
        _transform.position += speed * Time.deltaTime * target;
        // if (_transform.position == Vector3.zero)
        //     Destroy(this);
    }
}
