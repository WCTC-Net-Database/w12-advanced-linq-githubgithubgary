using ConsoleRpgEntities.Migrations;
using ConsoleRpgEntities.Models.Abilities.PlayerAbilities;
using ConsoleRpgEntities.Models.Attributes;
using ConsoleRpgEntities.Models.Equipments;

namespace ConsoleRpgEntities.Models.Characters;

public interface IPlayer
{
    int Id { get; set; }
    string Name { get; set; }
    ICollection<Ability> Abilities { get; set; }
    void Attack(ITargetable target);
    void UseAbility(IAbility ability, ITargetable target);
    bool SearchItemByName(string Name);
    void SearchItemByType(string Type);
    List<string> GetItemTypes();

    void SortItemsByName();
    void SortItemsByAttackValue();
    void SortItemsByDefenseValue();

    void UpdateWeightAllowance(int num);
    int GetWeightAllowance();
    void AddItem(string name);
}
