using System;
using System.Collections;
using UnityEngine;

namespace GlobalScripts
{
    public class PortalsLoading : MonoBehaviour
    {
        [SerializeField] public GameObject loadingScene;
        private LoadingBar _loading;
        
        private void Awake()
        {
            _loading = loadingScene.GetComponent<LoadingBar>();
        }

        private void Start()
        {
            _loading.loadingBar.color = new Color(0, 0, 0, 0);
            loadingScene.SetActive(false);
            _loading = loadingScene.GetComponent<LoadingBar>();
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                _loading.isFake = true;
                loadingScene.SetActive(true);
                StartCoroutine(ChangeColor());
            }
        }

        private IEnumerator ChangeColor()
        {
            yield return new WaitForSeconds(0.5f);
            _loading.loadingBar.color = new Color(255, 255, 255, 255);
        }
    }
}