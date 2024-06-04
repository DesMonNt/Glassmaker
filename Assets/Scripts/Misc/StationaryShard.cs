using System.Collections;
using System.Linq;
using Shards;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = System.Random;

namespace Misc
{
    public class StationaryShard : MonoBehaviour
    {
        public int key;
        private SpriteRenderer _renderer;
        private AudioSource _audioSource;
        [FormerlySerializedAs("IsBroken")] [FormerlySerializedAs("_isBroken")] public bool isBroken;
        [SerializeField] private Sprite pic;
        [SerializeField] private GameObject canvas;

        public GameObject crimsonShard;
        public GameObject azureShard;
        public GameObject amberShard;
    
        public Text crimsonShardText;
        public Text azureShardText;
        public Text amberShardText;


        private void Awake()
        {
            _renderer = GetComponent<SpriteRenderer>();
            _audioSource = GetComponent<AudioSource>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Player")) 
                return;
            _renderer.sprite = pic;
            if (isBroken) 
                return;
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

        private IEnumerator DestroyThis()
        {
            yield return new WaitForSeconds(8f);
            this.GameObject().SetActive(false);
        }
    }
}
