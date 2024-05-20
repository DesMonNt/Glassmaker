using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationaryShard : MonoBehaviour
{
    private SpriteRenderer _renderer;
    private AudioSource _audioSource;
    private bool _isBroken;
    [SerializeField] Sprite pic;
    [SerializeField] private GameObject canvas;


    void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _audioSource = GetComponent<AudioSource>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _renderer.sprite = pic;
            if (!_isBroken)
            {
                _audioSource.Play();
                Instantiate(canvas, new Vector3(0,0,0), new Quaternion());
                _isBroken = true;
            }
        }
    }
}
