using System.Collections.Generic;
using Effects;
using FightingScene;
using FightingScene.Units;

namespace AI
{
    public class BufferAI: AI
    {
        public BufferAI(Unit unit) : base(unit) { }
        
        public override (IAction, Unit target) MakeDecision(List<Unit> characters, List<Unit> enemies)
        {
            var action = GetAction();
            var target = Unit.CurrentStats.IsConfused
                ? action is Ability ? GetCharacterTarget(characters) : GetEnemyTarget(enemies)
                : action is Ability ? GetEnemyTarget(enemies) : GetCharacterTarget(characters);

            if (action is Attack && Unit.CurrentStats.IsConfused)
                target = GetCharacterTarget(characters);

            return (action, target);
        }
    }
}