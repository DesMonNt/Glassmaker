using System;
using System.Collections.Generic;
using Effects;

namespace FightingScene.Units
{
    public class Glassmaker : Unit
    {
        private bool _firstWish;
        private bool _secondWish;
        private bool _thirdWish;
        
        public Glassmaker() : base(new UnitStats(20000, 0.2f, 1100, 80, false, 0.1f, TypeOfAttack.Group, 0))
        {
            Ultimate = new Ability(new List<IBuff>(), 
                new List<IBuff> { new ShieldBuff((int)(0.1 * CurrentStats.MaxHealth)) }, "Хрустальный доспех");
        }

        public override Ability UseAbility()
        {
            if (_firstWish && _secondWish && _thirdWish)
            {
                _firstWish = false;
                _secondWish = false;
                _thirdWish = false;
            }

            if (!_firstWish)
            {
                _firstWish = true;
                
                return new Ability(new List<IBuff>(), new List<IBuff>(), "Первое желание: Сила");
            }
            
            if (!_firstWish)
            {
                _firstWish = true;
                
                return new Ability(new List<IBuff>(), new List<IBuff>(), "Первое желание: Жизнь");
            }

            _thirdWish = true;

            return new Ability(new List<IBuff>(), new List<IBuff>(), "Третье желание: Последнее")
            {
                Attack = new Attack((int)(CurrentStats.Damage * 3), Buffs, TypeOfAttack.Aoe)
            };
        }
    }
}