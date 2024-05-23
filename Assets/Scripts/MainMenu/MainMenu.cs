using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Application = UnityEngine.Device.Application;

public class MainMenu : MonoBehaviour
{  
    [SerializeField] private GameObject circle;
    [SerializeField] private GameObject[] texts;
    private float alphaCoeffitient = .5f;
    private float opacity1;
    
    [SerializeField] private GameObject _menuButtons;
    private float time;

    [SerializeField] private GameObject _startLine;
    [SerializeField] private GameObject _exitConformation;

    [SerializeField] private GameObject _blackout;

    private bool hadStarted = false;
    // Start is called before the first frame update
    void Start()
    {
        _menuButtons = GameObject.Find("MenuButtons");
        _menuButtons.SetActive(false);
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
        }
        
    }

    private async Task Appear(Graphic graphic, float duration)
    {
        var timer = 0f;
        var opacity = 0f;
        var color = graphic.color;
        graphic.color = new Color(color.r, color.g, color.b, 0);
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

    public void Show(GameObject obj)
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
