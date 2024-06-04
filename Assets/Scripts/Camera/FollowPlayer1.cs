using UnityEngine;

public class FollowPlayer1 : MonoBehaviour
{
    private Transform _player;
    private void Start() => _player = GameObject.FindGameObjectWithTag("Player").transform;

    private void LateUpdate()
    {
        var transform1 = transform;
        var savedTransform = transform1.position;
        var position = _player.position;
        (savedTransform.x, savedTransform.y) = (position.x, position.y);
        transform1.position = savedTransform;
    }
}
