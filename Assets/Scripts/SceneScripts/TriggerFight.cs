using System.Collections;
using System.Collections.Generic;
using FightingScene;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerFight : MonoBehaviour
{
    public List<GameObject> enemies;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SetedUnitsFromPreviousScene.SaveCharactersAndEnemies(enemies);
            SceneManager.LoadScene("FightingScene");
        }
    }
}
