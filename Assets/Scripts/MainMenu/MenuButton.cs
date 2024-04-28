using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour
{
    private Text _text;
    // [SerializeField]
    public Circle circle;
    [SerializeField]
    private Vector3 circlePos;
    
    // Start is called before the first frame update
    void Start()
    {
        circlePos = GetComponent<BoxCollider2D>().transform.position - new Vector3(0.60f, 0, 0);

    }

    void OnMouseEnter()
    {
        circle.endPos = circlePos;
        circle.startPos = circle.CurrentPos;
        circle.Time_ = 0;

        GetComponentInChildren<Text>().fontStyle = FontStyle.Bold;
    }
    
    void OnMouseExit()
    {
        GetComponentInChildren<Text>().fontStyle = FontStyle.Normal;
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
