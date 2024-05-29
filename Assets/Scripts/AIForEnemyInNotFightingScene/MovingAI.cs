using System;
using System.Collections.Generic;
using System.Linq;
using FightingScene;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using Random = System.Random;

public class MovingAI : MonoBehaviour
{
    public int key;
    private Collider2D _circleCollider;
    private CapsuleCollider2D _sphereCollider;
    private GameObject _player;
    private Collider2D _playerCollider;
    public List<GameObject> enemiesInFight;
    public bool isCanMove;
    private bool IsRun { get; set; }
    private bool _isStart;
    private Rigidbody2D _rb;
    
    private (int x, int y)[] _maybeCoordinates;

    private Vector2 _currentTarget;
    private Vector2 _startPosition;
    
    [FormerlySerializedAs("_speed")] public int speed;
    
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _maybeCoordinates = new[]
        {
            (0, 5), (5, 5), (5, 0), (5, -5), (0, -5), (-5, -5), 
            (-5, 0), (-5, -5), (0, 0)
        };
        _player = GameObject.FindWithTag("Player");
        _playerCollider = _player.GetComponent<Collider2D>();
        speed = 3;
        _startPosition = _rb.position;
        _currentTarget = _startPosition;
        _sphereCollider = GetComponent<CapsuleCollider2D>();
        _circleCollider = GetComponent<CircleCollider2D>();
    }

    private void Update()
    {
        if (_circleCollider.IsTouching(_playerCollider) && !_isStart)
        {
            Saves.playerPosition = _playerCollider.transform.position;
            Saves.Fights.Remove(key);
            _isStart = true;
            SetUnitsFromPreviousScene.SaveEnemies(enemiesInFight.ToList());
        }
        
        if (_sphereCollider.IsTouching(_playerCollider))
        {
            isCanMove = true;
            _currentTarget = _playerCollider.GameObject().transform.position;
            IsRun = true;
            GoToTarget(_currentTarget);
        }
        
        if (!isCanMove)
            return;
        
        if (!(Math.Abs(_currentTarget.x - _rb.position.x) < 1
              && Math.Abs(_currentTarget.y - _rb.position.y) < 1))
        {
            GoToTarget(_currentTarget);
        }
            
        else if (!IsRun) 
            _currentTarget = GetWalk();

        if (Vector2.Distance(_currentTarget, _rb.position) > 20)
        {
            IsRun = false;
            _currentTarget = GetWalk();
        }

        if (!_sphereCollider.IsTouching(_playerCollider)) 
            IsRun = false;
    }
    
    private void GoToTarget(Vector2 targetPosition)
    {
        var target = (targetPosition - _rb.position).normalized;
        _rb.MovePosition(_rb.position + target * (speed * Time.fixedDeltaTime));
    }

    private Vector2 GetWalk()
    {
        var randomValue = new Random().Next(0, 7); 
        var delta = new Vector2( 
            _rb.position.x + _maybeCoordinates[randomValue].x,
            _rb.position.y + _maybeCoordinates[randomValue].y);

        var a = _rb.position + delta - _startPosition;
            
        return Vector2.Distance(a, _startPosition) > 20 
            ? _startPosition
            : delta;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("Player"))
            _currentTarget = GetWalk();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Wall"))
            _currentTarget = GetWalk();
    }
}
