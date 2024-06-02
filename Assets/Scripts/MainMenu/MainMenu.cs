using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Application = UnityEngine.Device.Application;

public class MainMenu : MonoBehaviour
{  
    [SerializeField] private GameObject circle;
    [SerializeField] private GameObject[] texts;
    private float opacity1;
    
    [SerializeField] private GameObject _menuButtons; 
    private List<MenuButton> _menuButtonsList;
    private float time;

    [SerializeField] private GameObject _startLine;
    [SerializeField] private GameObject _exitConformation;

    [SerializeField] private GameObject _blackout;

    private bool hadStarted;

    private int selectedIndex;
    [SerializeField] private MenuButton _selected;

    private AudioSource _hoverSound;
    

    public MenuButton Selected
    {
        private get => _selected;
        set
        {
            _selected.GetComponentInChildren<Text>().fontStyle = FontStyle.Normal;
            _selected = value;
            _selected.GetComponentInChildren<Text>().fontStyle = FontStyle.Bold;

            selectedIndex = _menuButtonsList.IndexOf(_selected);
            var circleComp = circle.GetComponent<Circle>();
            circleComp.endPos = _selected.circlePos;

            circleComp.startPos = circleComp.CurrentPos;
            circleComp.Time_ = 0;
            _hoverSound.Play();
        }
    }

    void Start()
    {
        _selected = Selected;
        _hoverSound = GameObject.Find("HoverSound").GetComponent<AudioSource>();
        _menuButtons = GameObject.Find("MenuButtons");
        _menuButtons.SetActive(false);
        _menuButtonsList = new();
        foreach (var button in _menuButtons.transform.GetComponentsInChildren<MenuButton>())
        {
            _menuButtonsList.Add(button);
        }
        _startLine = GameObject.Find("StartLine");
        _exitConformation = GameObject.Find("ExitMenu");
        _exitConformation.SetActive(false);
        _blackout = GameObject.Find("Blackout");
        _blackout.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Show(_exitConformation);
        }
        if (Input.anyKey && !hadStarted)
        {
            hadStarted = true;
            _menuButtons.SetActive(true);
            var appearTime = 1f;
            foreach (var textComponent in texts.Select(x => x.GetComponent<Text>()))
            {
                Appear(textComponent, appearTime);
            }
            Appear(circle.GetComponent<Image>(), appearTime);
            _startLine.SetActive(false);
            Selected.SetPos();
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            selectedIndex++;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            selectedIndex--;
            
        }

        if (Input.anyKeyDown)
        {
            selectedIndex = (selectedIndex + _menuButtonsList.Count) % _menuButtonsList.Count;
            Selected = _menuButtonsList[selectedIndex];
        }
        
    }

    private async Task Appear(Graphic graphic, float duration)
    {
        var timer = 0f;
        var opacity = 0f;
        var color = graphic.color;
        graphic.color = new Color(color.r, color.g, color.b, 0);
        await Task.Delay(100);
        while (timer < duration)
        {
            
            graphic.color = new Color(color.r, color.g, color.b, opacity);
            opacity += Time.deltaTime / duration;
            timer += Time.deltaTime;

            await Task.Delay(10);
        }
    }
    void asfkjok()
    {
        PlayerPrefs.SetInt("hpBuff", 0);
        PlayerPrefs.SetInt("energyBuff", 0);
        SceneManager.LoadScene("SampleScene");
    }

    public void Exit()
    {
        Application.Quit();
    }

    private void Show(GameObject obj)
    {
        obj.SetActive(true);
        _blackout.SetActive(true);
        _menuButtons.SetActive(false);
    }
    
    public void Hide(GameObject obj)
    {
        obj.SetActive(false);
        _blackout.SetActive(false);
        _menuButtons.SetActive(true);
    }
}
