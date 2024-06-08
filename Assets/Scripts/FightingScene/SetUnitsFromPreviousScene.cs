using System.Collections.Generic;
using System.Linq;
using Effects;
using UnityEngine;
using Unit = FightingScene.Units.Unit;

namespace FightingScene
{
    public static class SetUnitsFromPreviousScene
    {
        private static readonly List<GameObject> CharactersPrefabs = new();
        private static List<GameObject> _enemiesPrefabs = new();

        public static float Volume = 0.4f;

        public static readonly List<IBuff> SavedShards = new();
        public static void SaveEnemies(IEnumerable<GameObject> enemies) => _enemiesPrefabs = enemies.ToList();

        public static void SaveCharacters(GameObject character) => CharactersPrefabs.Add(character);

        public static void SaveShard(IBuff shard) => SavedShards.Add(shard);

        public static (List<GameObject> characters, List<GameObject> enemies) SetCharactersAndEnemies()
        {
            var characters = CharactersPrefabs.ToList();
            var enemies = _enemiesPrefabs.ToList();
            
            foreach (var character in characters)
            {
                character.GetComponent<Unit>().RemoveAllBuffs();
            }
            
            return (characters, enemies);
        }
    }
}