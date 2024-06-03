#nullable enable

using System;
using System.Collections.Generic;
using Effects;
using FightingScene.Units;
using Random = System.Random;

namespace AI
{
    public abstract class AI
    {
        protected readonly Unit Unit;

        protected AI(Unit unit) => Unit = unit;

        public virtual (IAction, Unit target) MakeDecision(List<Unit> characters, List<Unit> enemies)
        {
            var action = GetAction();
            var target = !Unit.CurrentStats.IsConfused
                ? GetCharacterTarget(characters)
                : GetEnemyTarget(enemies);

            return (action, target);
        }

        protected IAction GetAction()
        {
            var actionNumber = new Random().Next(0, 100);

            return actionNumber switch
            {
                <= 40 => Unit.UseAttack(),
                <= 70 => Unit.UseAbility(),
                <= 100 => Unit.UseUltimate(),
                _ => throw new ArgumentException()
            };
        }

        protected virtual Unit GetCharacterTarget(List<Unit> characters)
        {
            var targetNumber = new Random().Next(0, characters.Count);

            return characters[targetNumber];
        }
        
        protected virtual Unit GetEnemyTarget(List<Unit> enemies)
        {
            var targetNumber = new Random().Next(0, enemies.Count);

            return enemies[targetNumber];
        }
    }
}