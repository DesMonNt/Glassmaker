using FightingScene;
using UnityEngine;

namespace SceneScripts
{
    public class SquadTrigger : MonoBehaviour
    {
        public int key;
        [SerializeField] public GameObject character;
        private bool _isUsed;

        private void OnTriggerEnter2D(Collider2D other)
        {
            Saves.Triggers[key] = true;
            if (!other.CompareTag("Player") || _isUsed) 
                return;
            _isUsed = true;
            SetUnitsFromPreviousScene.SaveCharacters(character);
        }
    }
}