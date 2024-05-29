using System.Collections.Generic;
using System.Linq;
using Effects;
using UnityEngine;
using Unit = FightingScene.Units.Unit;

namespace FightingScene
{
    public static class SetUnitsFromPreviousScene
    {
        private static List<GameObject> charactersPrefabs = new();
        private static List<GameObject> enemiesPrefabs = new();
        private static string folderName = "kskdjaknwb";

        public static List<IBuff> savedShards = new();
        public static void SaveEnemies(List<GameObject> enemies) => enemiesPrefabs = enemies.ToList();

        public static void SaveCharacters(GameObject character) => charactersPrefabs.Add(character);

        public static void SaveShard(IBuff shard) => savedShards.Add(shard);

        public static (List<GameObject> characters, List<GameObject> enemies) SetCharactersAndEnemies()
        {
            var characters = charactersPrefabs.ToList();
            var enemies = enemiesPrefabs.ToList();
            
            foreach (var character in characters)
            {
                character.GetComponent<Unit>().RemoveAllBuffs();
            }
            
            return (characters, enemies);
        }
    }
}