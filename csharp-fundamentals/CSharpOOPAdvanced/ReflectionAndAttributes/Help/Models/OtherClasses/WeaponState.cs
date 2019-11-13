using InfernoInfinity.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace InfernoInfinity.Models.OtherClasses
{
    public class WeaponState : IWeaponState
    {
        public WeaponState(int minDamage, int maxDamage, IGem[] numberOfSocket)
        {
            this.MinDamage = minDamage;
            this.MaxDamage = maxDamage;
            this.NumberOfSocket = numberOfSocket;
        }

        public int MinDamage { get;private set; }

        public int MaxDamage { get;private set; }

        public IGem[] NumberOfSocket { get; private set; }

        public void AddGem(IGem gem, int socketIndex)
        {
            this.NumberOfSocket[socketIndex] = gem;


            //TODO ТУК ИМА GEM ПРЕСМЯТАНИЯ


            //AddingTheGemPower(gem);
        }

        //private void AddingTheGemPower(IGem gem)
        //{
        //    this.MinDamage += gem.Strenght * 2;
        //    this.MaxDamage += gem.Strenght * 3;
        //    this.MinDamage += gem.Agility;
        //    this.MaxDamage += gem.Agility * 4;
        //}
    }
}
