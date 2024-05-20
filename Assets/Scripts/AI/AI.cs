#nullable enable

using System;
using System.Collections.Generic;
using Effects;

namespace AI
{
    public abstract class AI
    {
        private readonly Unit _unit;

        public AI(Unit unit) => _unit = unit;

        public virtual (IAction, Unit target) MakeDesicion(List<Unit> characters, List<Unit> enemies)
        {
            var action = GetAction();
            var target = GetCharacterTarget(characters);

            return (action, target);
        }

        protected IAction GetAction()
        {
            var actionNumber = new Random().Next(0, 3);

            return actionNumber switch
            {
                0 => _unit.UseAbility(),
                1 => _unit.UseUltimate(),
                2 => _unit.UseAttack(),
                _ => throw new ArgumentOutOfRangeException()
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