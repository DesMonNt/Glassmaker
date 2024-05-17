using System.Collections.Generic;
using Effects;

namespace AI
{
    public class BufferAI: AI
    {
        public BufferAI(Unit unit) : base(unit) { }
        
        public (IAction, Unit target) MakeDesicion(List<Character> characters, List<Enemy> enemies)
        {
            var action = GetAction();
            // var target = action is Ability
            //     ? GetCharacterTarget(characters) // вернуть обратно, не забыть
            //     : GetEnemyTarget(enemies);

            var target = GetCharacterTarget(characters);

            return (action, target);
        }
    }
}