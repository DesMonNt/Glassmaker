using System;
using System.Collections.Generic;
using Effects;

namespace FightingScene.Units
{
    public class MainCharacter : Unit
    {
        private int _criticalStack = 0;
        
        public MainCharacter() : base(new UnitStats(5500, 0.2f, 3000, 110, false, 0.1f, TypeOfAttack.Single, 3))
        {
            Skill = new Ability(new List<IBuff> { new SpeedBuff(-1.05f) }, new List<IBuff>(), "Багровая стрела")
            {
                Attack = new Attack(9000, Buffs, TypeOfAttack.Single)
            };

            Ultimate = new Ability(new List<IBuff>(), new List<IBuff>(), "Багровая стрела: Дождь")
            {
                Attack = new Attack(18000, Buffs, TypeOfAttack.Group)
            };
        }

        public override Ability UseAbility()
        {
            _criticalStack = Math.Clamp(0, _criticalStack + 1, 5);
            return Skill;
        }
        
        public override Ability UseUltimate()
        {
            Ultimate.Attack.Damage += 2500 * _criticalStack;
            _criticalStack = 0;
            return Ultimate;
        }
    }
}