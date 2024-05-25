﻿using System;
using System.Collections.Generic;
using Effects;

namespace FightingScene.Units
{
    public class MainCharacter : Unit
    {
        private int _criticalStack = 0;
        
        public MainCharacter() : base(new UnitStats(5500, 0.2f, 300, 110, false, 0.1f, TypeOfAttack.Single, 3))
        {
            Skill = new Ability(new List<IBuff>(), new List<IBuff>(), "Багровая стрела")
            {
                Attack = new Attack((int)(CurrentStats.Damage * 2.75), Buffs, TypeOfAttack.Single)
            };

            Ultimate = new Ability(new List<IBuff>(), new List<IBuff>(), "Багровая стрела: Дождь")
            {
                Attack = new Attack((int)(CurrentStats.Damage * 5.7), Buffs, TypeOfAttack.Group)
            };
        }

        public override Ability UseAbility()
        {
            _criticalStack = Math.Clamp(0, _criticalStack + 1, 5);
            AddBuff(new SpeedBuff(1.1f, 2));
            
            return Skill;
        }
        
        public override Ability UseUltimate()
        {
            Ultimate.Attack.Damage += 100 * _criticalStack;
            
            if (_criticalStack > 2)
                Ultimate.Attack.Damage += 100 * (_criticalStack - 2);
            
            _criticalStack = 0;
            
            return Ultimate;
        }
    }
}