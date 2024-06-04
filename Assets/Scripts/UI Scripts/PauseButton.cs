using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI_Scripts
{
    public class PauseButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private Text _text;

        private PauseMenu _pauseMenu;
    
        private void Start()
        {
            _pauseMenu = GameObject.Find("PauseMenu").GetComponent<PauseMenu>();

            var rect = GetComponent<RectTransform>();
            var position = rect.position;
            var x = position.x ;
            var y = position.y;
            new Vector3(x, y, 0);
        }

        public void OnPointerEnter(PointerEventData eventData) => _pauseMenu.Selected = GetComponent<PauseButton>();

        public void OnPointerExit(PointerEventData eventData) => GetComponentInChildren<Text>().fontStyle = FontStyle.Normal;
    }
}