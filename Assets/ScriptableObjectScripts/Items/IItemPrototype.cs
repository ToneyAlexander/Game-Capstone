namespace CCC.Items
{
    public interface IItemPrototype
    {
        // "name" is already a property of UnityEngine.Object and having the
        // property called "Name" would confuse it with "name". So, called
        // ItemName to avoid any potential confusion.
        string ItemName { get; }
        EquipmentSlot Slot { get; }

        int MaxHealth { get; }
        int MinHealth { get; }

        int MaxDamage { get; }
        int MinDamage { get; }

        int MaxArmor { get; }
        int MinArmor { get; }

        int MaxStrength { get; }
        int MinStrength { get; }

        int MaxDexterity { get; }
        int MinDexterity { get; }

        int MaxMysticism { get; }
        int MinMysticism { get; }
    }
}
