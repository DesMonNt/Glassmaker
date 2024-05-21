using System.Collections.Generic;
using Effects;
using UnityEngine;

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
        }
        public static void SetCharactersAndEnemies(List<GameObject> enemies, List<GameObject> characters)
        {
            
            // foreach (var characterPrefabPath in charactersPrefabsPaths)
            // {
            //     var characterPrefab = Resources.Load(characterPrefabPath) as GameObject;
            //     characters.Add(characterPrefab);
            // }
            //

            foreach (var gameObject in characters)
            {
                var character = gameObject.GetComponent<Unit>();
                foreach (var shard in savedShards)
                {
                    character.AddBuff(shard);
                }
            }
            foreach (var enemy in enemiesPrefabs)
            {
                enemies.Add(enemy);
            }
        }
    }
}