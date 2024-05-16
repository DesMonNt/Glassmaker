using System.Collections.Generic;
using System.Linq;
using Effects;

namespace AI
{
    public class HealerAI : AI
    {
        public HealerAI(Unit unit) : base(unit) { }
        
        public (IAction, Unit target) MakeDesicion(List<Character> characters, List<Enemy> enemies)
        {
            var action = GetAction();
            var target = action is Ability 
                ? GetEnemyTarget(enemies) 
                : GetCharacterTarget(characters);

            return (action, target);
        }
        protected override Unit GetEnemyTarget(List<Enemy> enemies)
        {
            foreach (var enemy in enemies.Where(enemy => !(enemy.currentHealthPoints >= enemy.CurrentStats.MaxHealth)))
                return enemy;

            return enemies[0];
        }
    }
}