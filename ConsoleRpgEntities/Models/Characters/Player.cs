using ConsoleRpgEntities.Models.Abilities.PlayerAbilities;
using ConsoleRpgEntities.Models.Attributes;
using ConsoleRpgEntities.Models.Equipments;
using Microsoft.IdentityModel.Tokens;
using ConsoleRpgEntities.Helpers;
using System.Xml.Linq;

namespace ConsoleRpgEntities.Models.Characters
{
    public class Player : ITargetable, IPlayer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Experience { get; set; }
        public int Health { get; set; }
        public int MaxWeight { get; set; }

        // Foreign key
        public int? EquipmentId { get; set; }

        // Navigation properties
        //public virtual Inventory Inventory { get; set; }
        public virtual List<Item> Items { get; set; }
        public virtual Equipment Equipment { get; set; }
        public virtual ICollection<Ability> Abilities { get; set; }

        public void Attack(ITargetable target)
        {
            // Player-specific attack logic
            Console.WriteLine($"{Name} attacks {target.Name} with a {Equipment.Weapon.Name} dealing {Equipment.Weapon.Attack} damage!");
            target.Health -= Equipment.Weapon.Attack;
            System.Console.WriteLine($"{target.Name} has {target.Health} health remaining.");
        }

        public void UseAbility(IAbility ability, ITargetable target)
        {
            if (Abilities.Contains(ability))
            {
                ability.Activate(this, target);
            }
            else
            {
                Console.WriteLine($"{Name} does not have the ability {ability.Name}!");
            }
        }

        public bool SearchItemByName(string name)
        {
            Item item = Items.Where(i => i.Name == name).FirstOrDefault();
            if (item != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public List<string> GetItemTypes()
        {
            List<string?> values = new List<string>();

            var items = Items.GroupBy(i => i.Type)
                        .Select(g => new { Type = g.Key })
                        .ToList();

            if (items.Count > 0)
            {
                //TODO
                // I could not get this to return the var list so this was the best that I could
                // do to get it back to the call routine
                int cnt = 0;
                string text = null;
                for (int i = 0; i< items.Count(); i++)
                {
                    text = $"{items[i].Type}";
                    values.Add(text);
                }
                values.Sort();
            }

            return values;
        }
        public void SearchItemByType(string type)
        {
            var items = Items.Where(i => i.Type == type);
            if (items != null)
            {
                foreach (var item in items)
                {
                    Console.WriteLine($"\t{item.Name}");
                }
            }
            else
            {
                Console.WriteLine($"\tNo items found of type {type}.");
            }
            return;
        }

        public void SortItemsByName()
        {
            var items = Items.OrderBy(i => i.Name).ToList();

            if (items != null)
            {
                foreach (var item in items)
                {
                    Console.WriteLine($"\tName: {item.Name}");
                }
            }
            else
            {
                Console.WriteLine($"\tNo items found.");
            }
            return;
        }
        public void SortItemsByAttackValue()
        {
            var items = Items
//                .GroupBy(x => new { x.Attack, x.Name })
//                .Select(g => new { Attack = g.Key, Name = g.Key.Name })
                .OrderByDescending(i => i.Attack)
                .ThenByDescending(i => i.Name)
                .ToList();

            if (items != null)
            {
                foreach (var item in items)
                {
                    Console.WriteLine($"\tName: {item.Name} \t\t\t\t\tAttack Value: {item.Attack}");
                    //                Console.WriteLine($"Name: {item.Name} \t\t\t\t\tAttack Value: {item.Attack.Attack}");
                }
            }
            else
            {
                Console.WriteLine($"\tNo items found.");
            }
            return;
        }
        public void SortItemsByDefenseValue() 
        {
            var items = Items
                .OrderBy(i => i.Defense)
                .ThenBy(i => i.Name)
                .ToList();

            if (items != null)
            {
                foreach (var item in items)
                {
                    Console.WriteLine($"\tName: {item.Name} \t\t\t\tDefense Value: {item.Defense}");
                }
            }
            else
            {
                Console.WriteLine($"\tNo items found.");
            }
            return;
        }
        public void AddItem(string name)
        {
            // Validate that the item name is part of the list of items that the player does not currently own.
            // total the weight of all of the players current items plus the new item
            // compare the total to the players maxweight
        }
        public void UpdateWeightAllowance(int num)
        {
            MaxWeight = num;
        }
        public int GetWeightAllowance()
        {
            return MaxWeight;
        }
    }
}
