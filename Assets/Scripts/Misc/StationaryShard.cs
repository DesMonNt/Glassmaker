using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class StationaryShard : MonoBehaviour
{
    private SpriteRenderer _renderer;
    private AudioSource _audioSource;
    private bool _isBroken;
    [SerializeField] Sprite pic;
    [SerializeField] private GameObject canvas;
    
    [SerializeField] public string crimsonShardBuff;
    [SerializeField] public string azureShardBuff;
    [SerializeField] public string amberShardBuff;

    [SerializeField] public GameObject crimsonShard;
    [SerializeField] public GameObject azureShard;
    [SerializeField] public GameObject amberShard;
    
    [SerializeField] public Text crimsonShardText;
    [SerializeField] public Text azureShardText;
    [SerializeField] public Text amberShardText;

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
                crimsonShardText.text = crimsonShardBuff;
                amberShardText.text = amberShardBuff;
                azureShardText.text = azureShardBuff;
                crimsonShard.SetActive(true);
                amberShard.SetActive(true);
                azureShard.SetActive(true);
                //Instantiate(canvas, new Vector3(0,0,0), new Quaternion());
                _isBroken = true;
            }
        }
    }
}
