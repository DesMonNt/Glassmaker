using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Application = UnityEngine.Device.Application;

public class MainMenu : MonoBehaviour
{  
    [SerializeField] private GameObject circle;
    [SerializeField] private GameObject[] texts;
    private float _opacity1;
    
    [FormerlySerializedAs("_menuButtons")] [SerializeField] private GameObject menuButtons; 
    private List<MenuButton> _menuButtonsList;
    private float _time;

    [FormerlySerializedAs("_startLine")] [SerializeField] private GameObject startLine;
    [FormerlySerializedAs("_exitConformation")] [SerializeField] private GameObject exitConformation;

    [FormerlySerializedAs("_blackout")] [SerializeField] private GameObject blackout;

    private bool _hadStarted;

    private int _selectedIndex;
    [FormerlySerializedAs("_selected")] [SerializeField] private MenuButton selected;

    private AudioSource _hoverSound;
    

    public MenuButton Selected
    {
        private get => selected;
        set
        {
            selected.GetComponentInChildren<Text>().fontStyle = FontStyle.Normal;
            selected = value;
            selected.GetComponentInChildren<Text>().fontStyle = FontStyle.Bold;

            _selectedIndex = _menuButtonsList.IndexOf(selected);
            var circleComp = circle.GetComponent<Circle>();
            circleComp.endPos = selected.circlePos;

            circleComp.startPos = circleComp.CurrentPos;
            circleComp.time = 0;
            _hoverSound.Play();
        }
    }

    void Start()
    {
        selected = Selected;
        _hoverSound = GameObject.Find("HoverSound").GetComponent<AudioSource>();
        menuButtons = GameObject.Find("MenuButtons");
        menuButtons.SetActive(false);
        _menuButtonsList = new();
        foreach (var button in menuButtons.transform.GetComponentsInChildren<MenuButton>())
        {
            _menuButtonsList.Add(button);
        }
        startLine = GameObject.Find("StartLine");
        exitConformation = GameObject.Find("ExitMenu");
        exitConformation.SetActive(false);
        blackout = GameObject.Find("Blackout");
        blackout.SetActive(false);

    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Show(exitConformation);
        }
        if (Input.anyKey && !_hadStarted)
        {
            _hadStarted = true;
            menuButtons.SetActive(true);
            const float appearTime = 1f;
            foreach (var textComponent in texts.Select(x => x.GetComponent<Text>()))
            {
                Appear(textComponent, appearTime);
            }
            Appear(circle.GetComponent<Image>(), appearTime);
            startLine.SetActive(false);
            Selected.SetPos();
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            _selectedIndex++;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            _selectedIndex--;
            
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            _selectedIndex = (_selectedIndex + _menuButtonsList.Count) % _menuButtonsList.Count;
            Selected = _menuButtonsList[_selectedIndex];
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

    public void Exit() => Application.Quit();

    private void Show(GameObject obj)
    {
        obj.SetActive(true);
        blackout.SetActive(true);
        menuButtons.SetActive(false);
    }
    
    public void Hide(GameObject obj)
    {
        obj.SetActive(false);
        blackout.SetActive(false);
        menuButtons.SetActive(true);
    }
}
