using UnityEngine;

public class FollowPlayer1 : MonoBehaviour
{
    private Transform _player;
    private void Start() => _player = GameObject.FindGameObjectWithTag("Player").transform;

    private void LateUpdate()
    {
        var temp = transform.position;
        (temp.x, temp.y) = (_player.position.x, _player.position.y);
        transform.position = temp;
    }
}
