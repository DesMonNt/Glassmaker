using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI_Scripts
{
    public class PauseMenu : MonoBehaviour
    {
        private List<PauseButton> _menuButtonsList;
        private float _time;

        private int _selectedIndex;
        [FormerlySerializedAs("_selected")] [SerializeField] private PauseButton selected;

        private AudioSource _hoverSound;

        private static bool _pauseGame;
        public GameObject pauseMenu;
        public List<GameObject> objectsToHide = new();

        public PauseMenu(List<PauseButton> menuButtonsList) => _menuButtonsList = menuButtonsList;

        public PauseButton Selected
        {
            private get => selected;
            set
            {
                selected.GetComponentInChildren<Text>().fontStyle = FontStyle.Normal;
                selected = value;
                selected.GetComponentInChildren<Text>().fontStyle = FontStyle.Bold;

                _selectedIndex = _menuButtonsList.IndexOf(selected);
                _hoverSound.Play();
            }
        }

        private void Start()
        {
            _hoverSound = GetComponent<AudioSource>();
            selected = Selected;
            _menuButtonsList = new();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.DownArrow))
                _selectedIndex++;
            else if (Input.GetKeyDown(KeyCode.UpArrow)) _selectedIndex--;

            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                _selectedIndex = (_selectedIndex + _menuButtonsList.Count) % _menuButtonsList.Count;
                Selected = _menuButtonsList[_selectedIndex];
            }

            if (!Input.GetKeyDown(KeyCode.Escape))
                return;
            if (_pauseGame)
                Resume();
            else
                Pause();
        }

        public void Resume()
        {
            foreach (var obj in objectsToHide) 
                obj.SetActive(true);

            pauseMenu.SetActive(false);
            Time.timeScale = 1f;
            _pauseGame = false;
        }

        private void Pause()
        {
            foreach (var obj in objectsToHide)
            {
                obj.SetActive(false);
            }

            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
            _pauseGame = true;
        }

        public void LoadMenu()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("MainMenu");
        }

        public void Exit() => Application.Quit();
    }
}