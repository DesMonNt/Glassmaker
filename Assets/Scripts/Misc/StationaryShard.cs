using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = System.Random;

public class StationaryShard : MonoBehaviour
{
    public int key;
    private SpriteRenderer _renderer;
    private AudioSource _audioSource;
    [FormerlySerializedAs("IsBroken")] [FormerlySerializedAs("_isBroken")] public bool isBroken;
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _renderer.sprite = pic;
            if (!isBroken)
            {
                Saves.ShardsIsBroken[key] = true;
                _audioSource.Play();
                
                var crimsonShardBuffs = BuffInfo.KeyToCrimsonBuff.Keys.ToList();
                var amberShardBuffs = BuffInfo.KeyToAmberBuff.Keys.ToList();
                var azureShardBuffs = BuffInfo.KeyToAzureBuff.Keys.ToList();

                var random = new Random();
                
                crimsonShardText.text = crimsonShardBuffs[random.Next(0, crimsonShardBuffs.Count)];
                amberShardText.text = amberShardBuffs[random.Next(0, amberShardBuffs.Count)];
                azureShardText.text = azureShardBuffs[random.Next(0, azureShardBuffs.Count)];
                
                canvas.SetActive(true);
                crimsonShard.SetActive(true);
                amberShard.SetActive(true);
                azureShard.SetActive(true);
                isBroken = true;

                StartCoroutine(DestroyThis());
            }
        }
    }

    private IEnumerator DestroyThis()
    {
        yield return new WaitForSeconds(8f);
        this.GameObject().SetActive(false);
    }
}
