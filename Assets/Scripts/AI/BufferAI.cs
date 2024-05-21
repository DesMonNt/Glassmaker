using System.Collections.Generic;
using Effects;
using FightingScene.Units;

namespace AI
{
    public class BufferAI: AI
    {
        public BufferAI(Unit unit) : base(unit) { }
        
        public (IAction, Unit target) MakeDesicion(List<Unit> characters, List<Unit> enemies)
        {
            var action = GetAction();
            var target = action is Ability 
                ? GetEnemyTarget(enemies) 
                : GetCharacterTarget(characters);

            return (action, target);
        }
    }
}