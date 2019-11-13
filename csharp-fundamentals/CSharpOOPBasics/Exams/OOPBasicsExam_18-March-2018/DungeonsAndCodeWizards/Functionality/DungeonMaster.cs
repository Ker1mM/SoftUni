using DungeonsAndCodeWizards.Factories;
using DungeonsAndCodeWizards.Interfaces;
using DungeonsAndCodeWizards.Messages;
using DungeonsAndCodeWizards.Models.Characters;
using DungeonsAndCodeWizards.Models.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DungeonsAndCodeWizards.Functionality
{
    public class DungeonMaster
    {
        private List<Character> party;
        private Stack<Item> itemPool;
        private int lastSurvivorRounds;

        public DungeonMaster()
        {
            this.party = new List<Character>();
            this.itemPool = new Stack<Item>();
            lastSurvivorRounds = 0;
        }

        public string JoinParty(string[] args)
        {
            string faction = args[0];
            string characterType = args[1];
            string name = args[2];

            var character = CharacterFactory.CreateCharacter(faction, characterType, name);
            party.Add(character);
            return string.Format(OutputMessages.JoinedParty(), name);
        }

        public string AddItemToPool(string[] args)
        {
            string itemName = args[0];
            var item = ItemFactory.CreateItem(itemName);
            itemPool.Push(item);

            return string.Format(OutputMessages.ItemAddedToPool(), itemName);
        }

        public string PickUpItem(string[] args)
        {
            string characterName = args[0];
            var character = party.FirstOrDefault(x => x.Name == characterName);
            Validate.ValidateCharacter(character, characterName);

            if (itemPool.Count == 0)
            {
                throw new InvalidOperationException(OutputMessages.EmptyPool());
            }

            character.ReceiveItem(itemPool.Peek());
            return string.Format(OutputMessages.PickedUpItem(), characterName, itemPool.Pop());
        }

        public string UseItem(string[] args)
        {
            string characterName = args[0];
            string itemName = args[1];

            var character = party.FirstOrDefault(x => x.Name == characterName);
            Validate.ValidateCharacter(character, characterName);

            Item item = character.Bag.GetItem(itemName);
            character.UseItem(item);

            return string.Format(OutputMessages.ItemUsed(), character.Name, itemName);
        }

        public string UseItemOn(string[] args)
        {
            string giverName = args[0];
            string receiverName = args[1];
            string itemName = args[2];

            var giver = party.FirstOrDefault(x => x.Name == giverName);
            Validate.ValidateCharacter(giver, giverName);
            var receiver = party.FirstOrDefault(x => x.Name == receiverName);
            Validate.ValidateCharacter(receiver, receiverName);

            Item item = giver.Bag.GetItem(itemName);
            giver.UseItemOn(item, receiver);

            return string.Format(OutputMessages.UseItemOn(), giverName, itemName, receiverName); //“{giverName} used {itemName} on {receiverName}.
        }

        public string GiveCharacterItem(string[] args)
        {
            string giverName = args[0];
            string receiverName = args[1];
            string itemName = args[2];

            var giver = party.FirstOrDefault(x => x.Name == giverName);
            Validate.ValidateCharacter(giver, giverName);
            var receiver = party.FirstOrDefault(x => x.Name == receiverName);
            Validate.ValidateCharacter(receiver, receiverName);

            Item item = giver.Bag.GetItem(itemName);
            giver.GiveCharacterItem(item, receiver);

            return string.Format(OutputMessages.GiveCharacterItem(), giverName, receiverName, itemName); //“{giverName} gave {receiverName} {itemName}.”
        }

        public string GetStats()
        {
            return string.Join(Environment.NewLine, party.OrderByDescending(x => x.IsAlive).ThenByDescending(x => x.Health));
        }

        public string Attack(string[] args)
        {
            string attackerName = args[0];
            string receiverName = args[1];

            var attacker = party.FirstOrDefault(x => x.Name == attackerName);
            Validate.ValidateCharacter(attacker, attackerName);
            var receiver = party.FirstOrDefault(x => x.Name == receiverName);
            Validate.ValidateCharacter(receiver, receiverName);

            Validate.CanAttack(attacker);

            var warrior = (Warrior)attacker;
            warrior.Attack(receiver);

            StringBuilder sb = new StringBuilder();
            sb.Append($"{attackerName} attacks {receiverName} for {attacker.AbilityPoints} hit points! ");
            sb.Append($"{receiverName} has {receiver.Health}/{receiver.BaseHealth} HP and {receiver.Armor}/{receiver.BaseArmor} AP left!");

            if (!receiver.IsAlive)
            {
                sb.AppendLine();
                sb.Append($"{receiver.Name} is dead!");
            }

            return sb.ToString();
        }

        public string Heal(string[] args)
        {
            string healerName = args[0];
            string healingReceiverName = args[1];

            var healer = party.FirstOrDefault(x => x.Name == healerName);
            Validate.ValidateCharacter(healer, healerName);
            var receiver = party.FirstOrDefault(x => x.Name == healingReceiverName);
            Validate.ValidateCharacter(receiver, healingReceiverName);

            Validate.CanHeal(healer);

            var cleric = (Cleric)healer;
            cleric.Heal(receiver);

            return $"{healer.Name} heals {receiver.Name} for {healer.AbilityPoints}! {receiver.Name} has {receiver.Health} health now!";
        }

        public string EndTurn(string[] args)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var character in party.Where(x => x.IsAlive))
            {
                double healthBeforeRest = character.Health;
                character.Rest();
                sb.AppendLine($"{character.Name} rests ({healthBeforeRest} => {character.Health})");
            }

            if (party.Where(x => x.IsAlive).Count() <= 1)
            {
                lastSurvivorRounds++;
            }

            return sb.ToString().TrimEnd();
        }

        public bool IsGameOver()
        {
            return lastSurvivorRounds > 1;
        }

    }
}
