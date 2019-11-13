using DungeonsAndCodeWizards.Models.Characters;

namespace DungeonsAndCodeWizards.Models.Items
{
    public class PoisonPotion : Item
    {
        private const int PoisonPotionWeight = 5;
        public PoisonPotion() : base(PoisonPotionWeight)
        {
        }

        public override void AffectCharacter(Character character)
        {
            base.AffectCharacter(character);
            character.TakeDirectDamage(20);
        }
    }
}
