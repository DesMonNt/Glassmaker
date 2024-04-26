using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;

    private PlayerContols _playerControls;
    private Vector2 _movement;
    private Rigidbody2D _rb;

    private void Awake()
    {
        _playerControls = new PlayerContols();
        _rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable() => _playerControls.Enable();

    private void Update() => PlayerInput();

    private void FixedUpdate() => Move();

    private void PlayerInput() => _movement = _playerControls.Movement.Move.ReadValue<Vector2>();

    private void Move() => _rb.MovePosition(_rb.position + _movement * (moveSpeed * Time.fixedDeltaTime));
}
