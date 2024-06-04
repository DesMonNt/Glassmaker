using UnityEngine;

namespace UI_Scripts
{
    public class OptionsScript : MonoBehaviour
    {
        public GameObject options;

        public GameObject menuButtons;

        public void ChangeButtons()
        {
            menuButtons.SetActive(false);
            options.SetActive(true);
        }
    }
}
