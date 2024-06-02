using FightingScene;
using UnityEngine;
using UnityEngine.UI;

public class VolumeScript : MonoBehaviour
{
    private Slider _slider;

    private void Start() => _slider = GetComponent<Slider>();

    private void Update() => SetUnitsFromPreviousScene.volume = _slider.value;
}
