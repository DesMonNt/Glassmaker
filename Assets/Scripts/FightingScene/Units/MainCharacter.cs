using System;
using System.Collections.Generic;
using Effects;

namespace FightingScene.Units
{
    public class MainCharacter : Unit
    {
        private int _criticalStack = 0;
        
        public MainCharacter() : base(new UnitStats(5500, 0.2f, 3000, 110, false, 0.1f, TypeOfAttack.Single))
        {
            skill = new Ability(new() { new SpeedBuff(-1.05f) }, new(), "Ебейшая пиздячка");
            skill.Attack = new Attack(9000, Buffs, TypeOfAttack.Single);

            ultimate = new Ability(new(), new(), "Ультра ебейшая пиздячка");
            ultimate.Attack = new Attack(18000, Buffs, TypeOfAttack.Group);
        }

        public override Ability UseAbility()
        {
            _criticalStack = Math.Clamp(0, _criticalStack + 1, 5);
            return skill;
        }
        
        public override Ability UseUltimate()
        {
            ultimate.Attack.Damage += 1500 * _criticalStack;
            _criticalStack = 0;
            return ultimate;
        }
    }
}