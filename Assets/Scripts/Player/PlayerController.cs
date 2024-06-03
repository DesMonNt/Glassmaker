using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;

    private PlayerContols _playerControls;
    private Vector2 _movement;
    private Rigidbody2D _rb;
    
    private Animator _playerAnimator;
    
    [SerializeField] public GameObject loadingScene;
    private LoadingBar _loading;
    [SerializeField] public string nameNextScene;

    private void Awake()
    {
        _playerAnimator = GetComponent<Animator>();
        _loading = loadingScene.GetComponent<LoadingBar>();
        _playerControls = new PlayerContols();
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _loading.loadingBar.color = new Color(0, 0, 0, 0);
        loadingScene.SetActive(false);
        _loading = loadingScene.GetComponent<LoadingBar>();
        _loading.nameOfScene = nameNextScene;
    }

    private void OnEnable() => _playerControls.Enable();

    private void Update(){
        if (!Input.anyKey)
            _playerAnimator.Play("StopAnimation");
        else
        {
            PlayerInput();
            Move();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("BigB") || other is not CircleCollider2D) 
            return;
        loadingScene.SetActive(true);
        StartCoroutine(ChangeColor());
    }

    private IEnumerator ChangeColor()
    {
        yield return new WaitForSeconds(0.5f);
        _loading.loadingBar.color = new Color(255, 255, 255, 255);
    }

    private void PlayerInput() => _movement = _playerControls.Movement.Move.ReadValue<Vector2>();

    private void Move()
    {
        _playerAnimator.Play("PlayerAnimation");
        _rb.MovePosition(_rb.position + _movement * (moveSpeed * Time.fixedDeltaTime));
    }
}
