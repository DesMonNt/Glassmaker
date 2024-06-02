using FightingScene;
using UnityEngine;

public class CheckerVolume : MonoBehaviour
{
    private AudioSource _audio;

    private void Start() => _audio = GetComponent<AudioSource>();

    private void Update() => _audio.volume = SetUnitsFromPreviousScene.volume;
}
