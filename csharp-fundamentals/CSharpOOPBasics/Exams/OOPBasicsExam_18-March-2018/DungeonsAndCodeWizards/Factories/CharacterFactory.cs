using DungeonsAndCodeWizards.Enums;
using DungeonsAndCodeWizards.Functionality;
using DungeonsAndCodeWizards.Messages;
using DungeonsAndCodeWizards.Models.Characters;
using System;

namespace DungeonsAndCodeWizards.Factories
{
    public class CharacterFactory
    {
        public static Character CreateCharacter(string faction, string type, string name)
        {
            Faction validatedFaction = Validate.ValidateFaction(faction);

            switch (type)
            {
                case "Warrior":
                    return new Warrior(name, validatedFaction);
                case "Cleric":
                    return new Cleric(name, validatedFaction);
                default:
                    throw new ArgumentException(string.Format(OutputMessages.InvalidCharacterType(), type));
            }
        }
    }
}
