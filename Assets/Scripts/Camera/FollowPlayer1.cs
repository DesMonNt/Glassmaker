using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer1 : MonoBehaviour
{
    private Transform _player;
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    
    void LateUpdate()
    {
        var temp = transform.position;
        (temp.x, temp.y) = (_player.position.x, _player.position.y);
        transform.position = temp;
    }
}
