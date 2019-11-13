namespace DungeonsAndCodeWizards.Models.Bags
{
    public class Backpack : Bag
    {
        private const int BackpackCapacity = 100;

        public Backpack() : base(BackpackCapacity)
        {
        }
    }
}
