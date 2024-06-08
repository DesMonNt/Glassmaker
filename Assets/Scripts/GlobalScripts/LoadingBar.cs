using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace GlobalScripts
{
    public class LoadingBar : MonoBehaviour
    {
        public bool isFake;
        public Image loadingBar;
        [FormerlySerializedAs("_percent")] public int percent;
        private float _square;
        public string nameOfScene;
    
        private void Start()
        {
            percent = 0;
            _square = 0;
        }

        private void FixedUpdate()
        {
            percent++;
            _square += GetFunc(percent);
            loadingBar.fillAmount = _square / 44.01f;
            if (percent != 100) 
                return;
            if (isFake)
            {
                percent = 0;
                gameObject.SetActive(false);
                loadingBar.color = new Color(0, 0, 0, 0);
                isFake = false;
            }
            
            else
            {
                SceneManager.LoadScene(nameOfScene);
                percent = 0;
                loadingBar.color = new Color(0, 0, 0, 0);
            }
        }

        private static float GetFunc(int x) => (float)Math.Pow(Math.E, -((0.04 * x - 2) * (0.04 * x - 2)));
    }
}