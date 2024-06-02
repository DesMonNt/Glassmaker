using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Text _text;

    [SerializeField] public Vector3 circlePos;

    private PauseMenu _pauseMenu;
    
    private void Start()
    {
        _pauseMenu = GameObject.Find("PauseMenu").GetComponent<PauseMenu>();

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

    public void OnPointerEnter(PointerEventData eventData)
    {
        _pauseMenu.Selected = GetComponent<PauseButton>();
    }
    
    public void OnPointerExit(PointerEventData eventData)
    {
        GetComponentInChildren<Text>().fontStyle = FontStyle.Normal;
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene("Tower exploration");
    }
}