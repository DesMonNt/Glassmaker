using System.Collections.Generic;
using Effects;
using UnityEngine;
using Unit = FightingScene.Units.Unit;

namespace FightingScene
{
    public static class SetedUnitsFromPreviousScene
    {
        private static List<GameObject> charactersPrefabs = new();
        private static List<GameObject> enemiesPrefabs = new();
        private static string folderName = "kskdjaknwb";

        private static List<IBuff> savedShards = new();
        public static void SaveEnemies(List<GameObject> enemies)
        {
            foreach (var enemyPrefab in enemies)
            {
                enemiesPrefabs.Add(enemyPrefab);
            }
        }

        public static void SaveCharacters(GameObject character) => charactersPrefabs.Add(character);

        public static void SaveShard(IBuff shard)
        {
            savedShards.Add(shard);
        }
        public static void SetCharactersAndEnemies(List<GameObject> enemies, List<GameObject> characters)
        {
            foreach (var character in charactersPrefabs)
            {
                character.GetComponent<Unit>().RemoveAllBuffs();
            }
            

            foreach (var gameObject in characters)
            {
                var character = gameObject.GetComponent<Unit>();
                foreach (var shard in savedShards)
                {
                    character.AddBuff(shard);
                }
            }
            
            
            // Раскомментить снизу, чтобы все работало, но на сцену нужно заходить именно с SampleScene,
            // а не сразу, иначе врагов не будет
            
            characters.Clear();
            characters.AddRange(charactersPrefabs);
            
            enemies.Clear();
            enemies.AddRange(enemiesPrefabs);
        }
    }
}