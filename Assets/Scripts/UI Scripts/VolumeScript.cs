using FightingScene;
using UnityEngine;
using UnityEngine.UI;

namespace UI_Scripts
{
    public class VolumeScript : MonoBehaviour
    {
        private Slider _slider;

        private void Start() => _slider = GetComponent<Slider>();

        private void Update() => SetUnitsFromPreviousScene.Volume = _slider.value;
    }
}
