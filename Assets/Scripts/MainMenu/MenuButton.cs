using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;
using UnityEngine.Video;

public class MenuButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Text _text;

    public Vector3 circlePos;

    private MainMenu _mainMenu;
    
    private void Start()
    {
        _mainMenu = GameObject.Find("Menu").GetComponent<MainMenu>();
        var rect = GetComponent<RectTransform>();
        var position = rect.position;
        var x = position.x ;
        var y = position.y;
        circlePos = new Vector3(x, y, 0);
    }

    public void SetPos()
    {
        var rect = GetComponent<RectTransform>();
        var position = rect.position;
        var x = position.x ;
        var y = position.y;
        circlePos = new Vector3(x, y, 0);
    }

    public void OnPointerEnter(PointerEventData eventData) => _mainMenu.Selected = GetComponent<MenuButton>();

    public void OnPointerExit(PointerEventData eventData) => GetComponentInChildren<Text>().fontStyle = FontStyle.Normal;

    public void ChangeScene() => SceneManager.LoadScene("Tower exploration");
}
