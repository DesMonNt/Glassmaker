using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

namespace UI_Scripts
{
    public class IntroScript : MonoBehaviour
    {
        public GameObject video;
        private VideoPlayer _videoPlayer;
        private ulong _frames;
        private bool _isPlayed;
        public List<GameObject> buttons = new();
    
        private void Start()
        {
            _videoPlayer = video.GetComponent<VideoPlayer>();
            _frames = _videoPlayer.frameCount;
        }
    
        public void StartGame()
        {
            video.SetActive(true);
            foreach (var button in buttons)
                button.SetActive(false);
            _isPlayed = true;
        }

        private void Update()
        {
            if (!_isPlayed)
                return;
            if (Input.anyKeyDown || (ulong)(_videoPlayer.frame + 2) >= _frames) 
                SceneManager.LoadScene("Tower exploration");
        }
    }
}
