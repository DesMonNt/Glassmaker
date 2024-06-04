using System;
using System.Collections.Generic;
using System.Linq;
using FightingScene;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using Random = System.Random;

namespace AIForEnemyInNotFightingScene
{
    public class MovingAI : MonoBehaviour
    {
        public int key;
        private Collider2D _circleCollider;
        private CapsuleCollider2D _sphereCollider;
        private GameObject _player;
        private Collider2D _playerCollider;
        public List<GameObject> enemiesInFight;
        public bool canMove;
        private bool IsRun { get; set; }
        private bool _isStart;
        private Rigidbody2D _rb;
    
        private (int x, int y)[] _maybeCoordinates;

        private Vector2 _currentTarget;
        private Vector2 _startPosition;
    
        [FormerlySerializedAs("_speed")] public int speed;

        private Animator _animator;
        public Sprite additionalSprite;
    
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
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            if (_circleCollider.IsTouching(_playerCollider) && !_isStart)
            {
                Saves.PlayerPosition = _playerCollider.transform.position;
                Saves.Fights.Remove(key);
                _isStart = true;
                SetUnitsFromPreviousScene.SaveEnemies(enemiesInFight.ToList());
            }
        
            if (_sphereCollider.IsTouching(_playerCollider))
            {
                canMove = true;
                _currentTarget = _playerCollider.GameObject().transform.position;
                IsRun = true;
                GoToTarget(_currentTarget);
            }
        
            if (!canMove)
                return;
        
            _animator.Play(name);
        
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
            var position = _rb.position;
            var target = (targetPosition - position).normalized;
            _rb.MovePosition(position + target * (speed * Time.fixedDeltaTime));
        }

        private Vector2 GetWalk()
        {
            var randomValue = new Random().Next(0, 7);
            var position = _rb.position;
            var delta = new Vector2( 
                position.x + _maybeCoordinates[randomValue].x,
                position.y + _maybeCoordinates[randomValue].y);
            var newPosition = position + delta - _startPosition;
            
            return Vector2.Distance(newPosition, _startPosition) > 20 
                ? _startPosition
                : delta;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (!other.gameObject.CompareTag("Player"))
                _currentTarget = GetWalk();
            if (additionalSprite != null)
                GetComponent<SpriteRenderer>().sprite = additionalSprite;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Wall"))
                _currentTarget = GetWalk();
        }
    }
}
