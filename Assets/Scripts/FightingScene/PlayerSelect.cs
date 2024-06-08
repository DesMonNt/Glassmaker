using UnityEngine;

namespace FightingScene
{
    public class PlayerSelect : MonoBehaviour
    {
        private GameObject[] _characters;
        private int _index;
    
        private void Start()
        {
            _index = PlayerPrefs.GetInt("CharacterSelected");
            _characters = new GameObject[transform.childCount];

            for (var i = 0; i < transform.childCount; i++) 
                _characters[i] = transform.GetChild(i).gameObject;
            foreach (var character in _characters) 
                character.SetActive(false);

            if (_characters[_index]) 
                _characters[_index].SetActive(true);
        }
    }
}