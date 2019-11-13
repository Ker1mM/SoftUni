using DungeonsAndCodeWizards.Enums;
using DungeonsAndCodeWizards.Messages;
using DungeonsAndCodeWizards.Models.Characters;
using System;
using System.Collections.Generic;

namespace DungeonsAndCodeWizards.Functionality
{
    internal static class Validate
    {
        internal static Faction ValidateFaction(string faction)
        {
            bool result = Enum.TryParse(typeof(Faction), faction, out object factionObj);

            if (!result)
            {
                throw new ArgumentException(string.Format(OutputMessages.InvalidWeatherType(), faction));
            }

            return (Faction)factionObj;
        }

        internal static void ValidateCharacter(Character character, string name)
        {
            if (character == null)
            {
                throw new ArgumentException(string.Format(OutputMessages.CharacterNotFound(), name));
            }
        }

        internal static void CanAttack(Character character)
        {
            if (character.GetType().Name != "Warrior")
            {
                throw new ArgumentException(string.Format(OutputMessages.CantAttack(), character.Name));
            }
        }

        internal static void CanHeal(Character character)
        {
            if (character.GetType().Name != "Cleric")
            {
                throw new ArgumentException(string.Format(OutputMessages.CantHeal(), character.Name));
            }
        }
    }
}
