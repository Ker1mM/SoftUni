namespace DungeonsAndCodeWizards.Messages
{
    public static class OutputMessages
    {
        public static string DeadCharacter() => "Must be alive to perform this action!";
        public static string FullBag() => "Bag is full!";
        public static string EmptyBag() => "Bag is empty!";
        public static string NoItemWithThatName() => "No item with name {0} in bag!";
        public static string NullOrWhitespaceName() => "Name cannot be null or whitespace!";
        public static string CantAttackSelf() => "Cannot attack self!";
        public static string SameFaction() => "Friendly fire! Both characters are from {0} faction!";
        public static string DifferentFaction() => "Cannot heal enemy character!";
        public static string InvalidCharacterType() => "Invalid character type \"{0}\"!";
        public static string InvalidItemType() => "Invalid item \"{0}\"!";
        public static string InvalidWeatherType() => "Invalid faction \"{0}\"!";
        public static string JoinedParty() => "{0} joined the party!";
        public static string ItemAddedToPool() => "{0} added to pool.";
        public static string CharacterNotFound() => "Character {0} not found!";
        public static string EmptyPool() => "No items left in pool!";
        public static string PickedUpItem() => "{0} picked up {1}!";
        public static string ItemUsed() => "{0} used {1}.";
        public static string UseItemOn() => "{0} used {1} on {2}.";
        public static string GiveCharacterItem() => "{0} gave {1} {2}.";
        public static string CantAttack() => "{0} cannot attack!";
        public static string CantHeal() => "{0} cannot heal!";
    }
}
