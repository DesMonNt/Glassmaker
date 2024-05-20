using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{  
    [SerializeField] private GameObject circle;
    [SerializeField] private Image circleComponent;
    [SerializeField] private GameObject[] texts;
    [SerializeField] private Text[] textsComponents;
    private float alphaCoeffitient = .5f;
    private float opacity;
    
    [SerializeField] private GameObject _menuButtons;
    private float time;

    [SerializeField] private GameObject _startLine;

    private bool hadStarted = false;
    // Start is called before the first frame update
    void Start()
    {
        textsComponents = texts.Select(x => x.GetComponent<Text>()).ToArray();
        circleComponent = circle.GetComponent<Image>();
        _menuButtons = GameObject.Find("MenuButtons");
        _menuButtons.SetActive(false);
        _startLine = GameObject.Find("StartLine");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
        if (Input.anyKey && !hadStarted)
        {
            _menuButtons.SetActive(true);
            foreach (var textComponent in textsComponents)
            {
                textComponent.color = new Color(255, 255, 255, 0);
            }
            circleComponent.color = new Color(255, 255, 255, 0);
            _startLine.SetActive(false);
            hadStarted = true;
        }
        
        else if (hadStarted && opacity < 255)
        {
            foreach (var textComponent in textsComponents)
            {
                textComponent.color = new Color(255, 255, 255, opacity);
            }
            circleComponent.color = new Color(255, 255, 255, opacity);

            opacity += Time.deltaTime * alphaCoeffitient;

        }
        
    }
    void asfkjok()
    {
        PlayerPrefs.SetInt("hpBuff", 0);
        PlayerPrefs.SetInt("energyBuff", 0);
        SceneManager.LoadScene("SampleScene");
    }
}
