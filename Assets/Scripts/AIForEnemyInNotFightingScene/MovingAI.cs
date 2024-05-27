using System;
using System.Collections.Generic;
using System.Collections.Generic;
using FightingScene;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = System.Random;
using Timer = System.Timers.Timer;

public class MovingAI : MonoBehaviour
{
    private Collider2D _boxCollider;
    private CapsuleCollider2D _sphereCollider;
    private GameObject _player;
    private Collider2D _playerCollider;
    private System.Timers.Timer _timer;
    private SpriteRenderer _renderer;
    public List<GameObject> enemiesInFight;
    public bool IsRun { get; set; }
    private bool _isStart;
    private Rigidbody2D _rb;
    
    private (int x, int y)[] _maybeCoordinates;

    private Vector2 _currentTarget;
    private Vector2 _startPosition;
    
    private Transform _transform;
    public int _speed;
    
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _renderer = GetComponent<SpriteRenderer>();
        _timer = new ();
        _maybeCoordinates = new[]
        {
            (0, 5), (5, 5), (5, 0), (5, -5), (0, -5), (-5, -5), 
            (-5, 0), (-5, -5), (0, 0)
        };
        _player = GameObject.FindWithTag("Player");
        _playerCollider = _player.GetComponent<Collider2D>();
        _speed = 3;
        _transform = transform;
        _startPosition = _rb.position;
        _currentTarget = _startPosition;
        _boxCollider = GetComponent<BoxCollider2D>();
        _sphereCollider = GetComponent<CapsuleCollider2D>();
    }

    private void Update()
    {
        if (!(Math.Abs(_currentTarget.x - _rb.position.x) < 1
              && Math.Abs(_currentTarget.y - _rb.position.y) < 1))
        {
            GoToTarget(_currentTarget);
        }
            
        else if (!IsRun) 
            _currentTarget = GetWalk();
        
        if (_sphereCollider.IsTouching(_playerCollider))
        {
            _currentTarget = _playerCollider.GameObject().transform.position;
            IsRun = true;
            GoToTarget(_currentTarget);
        }

        if (Vector2.Distance(_currentTarget, _rb.position) > 20)
        {
            IsRun = false;
            _currentTarget = GetWalk();
        }

        if (!_sphereCollider.IsTouching(_playerCollider))
        {
            IsRun = false;
        }
        
        if (_boxCollider.IsTouching(_playerCollider) && !_isStart)
        {
            _isStart = true;
            SetedUnitsFromPreviousScene.SaveCharactersAndEnemies(enemiesInFight);
        }
    }
    
    private void GoToTarget(Vector2 targetPosition)
    {
        // if (Vector2.Distance(targetPosition, _rb.position) > 20)
        //     GetWalk();
        var target = (targetPosition - _rb.position).normalized;
        _rb.MovePosition(_rb.position + target * (_speed * Time.fixedDeltaTime));
    }

    private Vector2 GetWalk()
    {
        var randomValue = new Random().Next(0, 7); 
        var delta = new Vector2( 
            _rb.position.x + _maybeCoordinates[randomValue].x,
            _rb.position.y + _maybeCoordinates[randomValue].y);

        var a = _rb.position + delta - _startPosition;
            
        return Math.Abs(a.x * a.x + a.y * a.y - 10 * 10) < 1e-5 
            ? GetWalk() 
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
