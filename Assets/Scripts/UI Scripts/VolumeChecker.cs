using FightingScene;
using UnityEngine;

namespace UI_Scripts
{
    public class VolumeChecker : MonoBehaviour
    {
        private AudioSource _audio;

        private void Start() => _audio = GetComponent<AudioSource>();

        private void Update() => _audio.volume = SetUnitsFromPreviousScene.Volume;
    }
}