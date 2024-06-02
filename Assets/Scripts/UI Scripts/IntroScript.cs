using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class IntroScript : MonoBehaviour
{
    public GameObject video;
    private VideoPlayer _videoPlayer;
    private ulong _frames;
    private bool _isPlay;
    public GameObject buttons;
    
    private void Start()
    {
        _videoPlayer = video.GetComponent<VideoPlayer>();
        _frames = _videoPlayer.frameCount;
    }
    
    public void StartGame()
    {
        video.SetActive(true);
        buttons.SetActive(false);
        _isPlay = true;
    }

    private void Update()
    {
        if (!_isPlay)
            return;
        if (Input.anyKeyDown || (ulong)(_videoPlayer.frame + 2) >= _frames) SceneManager.LoadScene("Tower exploration");
    }
}
