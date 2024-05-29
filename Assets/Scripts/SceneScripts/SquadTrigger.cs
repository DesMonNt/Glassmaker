using FightingScene;
using UnityEngine;

namespace SceneScripts
{
    public class SquadTrigger : MonoBehaviour
    {
        [SerializeField] public GameObject character;
        private bool _isUsed;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Player") || _isUsed) 
                return;
            _isUsed = true;
            SetUnitsFromPreviousScene.SaveCharacters(character);
        }
    }
}