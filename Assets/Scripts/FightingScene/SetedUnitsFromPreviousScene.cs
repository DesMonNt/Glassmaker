using System.Collections.Generic;
using Effects;
using UnityEngine;
using Unit = FightingScene.Units.Unit;

namespace FightingScene
{
    public static class SetedUnitsFromPreviousScene
    {
        private static List<GameObject> charactersPrefabs;
        private static List<GameObject> enemiesPrefabs = new();
        private static string folderName = "kskdjaknwb";

        private static List<IBuff> savedShards = new();
        public static void SaveCharactersAndEnemies(List<GameObject> enemies)
        {
            foreach (var enemyPrefab in enemies)
            {
                enemiesPrefabs.Add(enemyPrefab);
            }
        }

        public static void SaveShard(IBuff shard)
        {
            savedShards.Add(shard);
            Debug.Log($"{savedShards.Count}");
        }
        
        public static void SetCharactersAndEnemies(List<GameObject> enemies, List<GameObject> characters = null)
        {
            
            // foreach (var characterPrefabPath in charactersPrefabsPaths)
            // {
            //     var characterPrefab = Resources.Load(characterPrefabPath) as GameObject;
            //     characters.Add(characterPrefab);
            // }
            //

            // foreach (var gameObject in characters)
            // {
            //     var character = gameObject.GetComponent<Unit>();
            //     foreach (var shard in savedShards)
            //     {
            //         character.AddBuff(shard);
            //     }
            // }
            
            
            // Раскомментить снизу, чтобы все работало, но на сцену нужно заходить именно с SampleScene,
            // а не сразу, иначе врагов не будет
            
            enemies.Clear();
            foreach (var enemy in enemiesPrefabs)
            {
                enemies.Add(enemy);
            }
        }
    }
}