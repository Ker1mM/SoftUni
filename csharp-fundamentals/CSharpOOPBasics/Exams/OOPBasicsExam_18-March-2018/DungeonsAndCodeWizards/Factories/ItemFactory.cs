using DungeonsAndCodeWizards.Messages;
using DungeonsAndCodeWizards.Models.Items;
using System;

namespace DungeonsAndCodeWizards.Factories
{
    public class ItemFactory
    {
        public static Item CreateItem(string type)
        {
            switch (type)
            {
                case "PoisonPotion":
                    return new PoisonPotion();
                case "HealthPotion":
                    return new HealthPotion();
                case "ArmorRepairKit":
                    return new ArmorRepairKit();
                default:
                    throw new ArgumentException(string.Format(OutputMessages.InvalidItemType(), type));
            }
        }
    }
}
