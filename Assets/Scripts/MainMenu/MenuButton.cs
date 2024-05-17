using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Text _text;
    [SerializeField] private AudioSource sound;

    // [SerializeField]
    public Circle circle;

    [SerializeField]
    public Vector3 circlePos{ get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        var boxcollider = GetComponent<BoxCollider2D>();
        var rect = GetComponent<RectTransform>();
        sound = GameObject.Find("HoverSound").GetComponent<AudioSource>();
        var position = rect.position;
        var x = rect.position.x ;
        var y = position.y;
        circlePos = new Vector3(x, y, 0);

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        circle.endPos = circlePos;
        circle.startPos = circle.CurrentPos;
        circle.Time_ = 0;
        sound.Play();
        GetComponentInChildren<Text>().fontStyle = FontStyle.Bold;
    }
    
    public void OnPointerExit(PointerEventData eventData)
    {
        GetComponentInChildren<Text>().fontStyle = FontStyle.Normal;
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
