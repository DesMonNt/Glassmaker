using System.Collections.Generic;
using FightingScene;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SceneScripts
{
    public class TriggerFight : MonoBehaviour
    {
        public List<GameObject> enemies;

        public void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Player")) 
                return;
            SetUnitsFromPreviousScene.SaveEnemies(enemies);
            SceneManager.LoadScene("FightingScene");
        }
    }
}
