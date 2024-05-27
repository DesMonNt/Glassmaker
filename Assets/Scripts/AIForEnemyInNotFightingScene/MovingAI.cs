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
    
    private (int x, int y)[] _maybeCoordinates;

    private Vector3 _currentTarget;
    private Vector3 _startPosition;
    
    private Transform _transform;
    public int _speed;
    
    private void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _timer = new ();
        _currentTarget = new Vector3(0, 0, 0);
        _maybeCoordinates = new[]
        {
            (1, 1), (1, 1), (1, 0), (1, -1), (0, -1), (-1, -1), 
            (-1, 0), (-1, 10), (0, 0)
        };
        _player = GameObject.FindWithTag("Player");
        _playerCollider = _player.GetComponent<Collider2D>();
        _speed = 3;
        _transform = transform;
        _startPosition = _transform.position;
        _boxCollider = GetComponent<BoxCollider2D>();
        _sphereCollider = GetComponent<CapsuleCollider2D>();
    }

    private void Update()
    {
        if (!(Math.Abs(_currentTarget.x - _transform.position.x) < 1
              && Math.Abs(_currentTarget.y - _transform.position.y) < 1))
        {
            GoToTarget(_currentTarget);
        }
            
        else if (!IsRun) 
            _currentTarget = GetWalk();
        

        if (_sphereCollider.IsTouching(_playerCollider))
        {
            IsRun = true;
            _currentTarget = _playerCollider.GameObject().transform.position;
            GoToTarget(_player.GameObject().transform.position);
        }
        
        if (!_sphereCollider.IsTouching(_playerCollider)) 
            IsRun = false;

        if (_boxCollider.IsTouching(_playerCollider) && !_isStart)
        {
            _isStart = true;
            SetedUnitsFromPreviousScene.SaveCharactersAndEnemies(enemiesInFight);
        }
    }
    
    private void GoToTarget(Vector3 targetPosition)
    {
        var target = (targetPosition - _transform.position).normalized;
        _transform.position += _speed * Time.deltaTime * target;
    }

    private Vector3 GetWalk()
    {
        var randomValue = new Random().Next(0, 7); 
        var delta = new Vector3(
            _transform.position.x + _maybeCoordinates[randomValue].x,
            _transform.position.y + _maybeCoordinates[randomValue].y);
        var a = _transform.position + delta - _startPosition;
        return a.x * a.x + a.y * a.y - _sphereCollider.size.y * _sphereCollider.size.y < 1e-5 
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
