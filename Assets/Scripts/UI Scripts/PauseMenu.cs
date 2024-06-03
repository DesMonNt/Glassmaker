using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject[] texts;

    private List<PauseButton> _menuButtonsList;
    private float time;

    private int selectedIndex;
    [SerializeField] private PauseButton _selected;

    private AudioSource _hoverSound;
    
    public static bool PauseGame;
    public GameObject pauseMenu;
    public List<GameObject> objectsToHide = new();

    public PauseButton Selected
    {
        private get => _selected;
        set
        {
            _selected.GetComponentInChildren<Text>().fontStyle = FontStyle.Normal;
            _selected = value;
            _selected.GetComponentInChildren<Text>().fontStyle = FontStyle.Bold;

            selectedIndex = _menuButtonsList.IndexOf(_selected);
            _hoverSound.Play();
        }
    }

    void Start()
    {
        _hoverSound = GetComponent<AudioSource>();
        _selected = Selected;
        _menuButtonsList = new();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            selectedIndex++;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            selectedIndex--;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            selectedIndex = (selectedIndex + _menuButtonsList.Count) % _menuButtonsList.Count;
            Selected = _menuButtonsList[selectedIndex];
        }

        if (!Input.GetKeyDown(KeyCode.Escape))
            return;
        if (PauseGame)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }

    public void Resume()
    {
        foreach (var obj in objectsToHide)
        {
            obj.SetActive(true);
        }

        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        PauseGame = false;
    }

    public void Pause()
    {
        foreach (var obj in objectsToHide)
        {
            obj.SetActive(false);
        }

        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        PauseGame = true;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
    
    public void LoadFakeMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("FakeMenu");
    }

    public void Exit()
    {
        Application.Quit();
    }
}