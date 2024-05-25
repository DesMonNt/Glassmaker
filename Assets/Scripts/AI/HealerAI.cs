using System.Collections.Generic;
using System.Linq;
using Effects;
using FightingScene.Units;
using UnityEngine;

namespace AI
{
    public class HealerAI : AI
    {
        public HealerAI(Unit unit) : base(unit) { }
        
        public override (IAction, Unit target) MakeDecision(List<Unit> characters, List<Unit> enemies)
        {
            var action = GetAction();
            var target = action is Ability && !Unit.CurrentStats.IsConfused
                ? GetEnemyTarget(enemies) 
                : GetCharacterTarget(characters);

            return (action, target);
        }
        protected override Unit GetEnemyTarget(List<Unit> enemies)
        {
            foreach (var enemy in enemies.Where(enemy => enemy.currentHealthPoints <= enemy.CurrentStats.MaxHealth * 0.5))
                return enemy;

            return base.GetEnemyTarget(enemies);
        }
    }
}