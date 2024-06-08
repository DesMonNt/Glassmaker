using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace MainMenuLogic
{
    public class MenuButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private Text _text;

        [FormerlySerializedAs("circlePos")] public Vector3 circlePosition;
        private RectTransform _rect;

        private MainMenu _mainMenu;
    
        private void Start()
        {
            _rect = GetComponent<RectTransform>();
            _mainMenu = GameObject.Find("Menu").GetComponent<MainMenu>();
            var rect = GetComponent<RectTransform>();
            var position = rect.position;
            var x = position.x ;
            var y = position.y;
            circlePosition = new Vector3(x, y, 0);
        }

        public void SetPosition()
        {
            var position = _rect.position;
            var x = position.x ;
            var y = position.y;
            circlePosition = new Vector3(x, y, 0);
        }

        public void OnPointerEnter(PointerEventData eventData) => _mainMenu.Selected = GetComponent<MenuButton>();

        public void OnPointerExit(PointerEventData eventData) => GetComponentInChildren<Text>().fontStyle = FontStyle.Normal;
    }
}
