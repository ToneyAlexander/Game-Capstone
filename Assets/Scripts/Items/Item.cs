using UnityEngine;
using System.Collections.Generic;

using CCC.Stats;

namespace CCC.Items
{
    /// <summary>
    /// Represents an item in the game.
    /// </summary>
    public class Item
    {
        /// <summary>
        /// The null Item that is used when no other Item makes sense.
        /// </summary>
        public static Item Null = new Item(
            "Null Item", 
            "You probably shouldn't have this.",
            true,
            1,
            EquipmentSlot.Null,
            null,
            new List<Stat>(),
            null
        );

        /// <summary>
        /// Gets the name of this Item.
        /// </summary>
        /// <value>The name.</value>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string LongName
        {
            get { return longName; }
            set { longName = value; }
        }

        /// <summary>
        /// Gets the flavor text of this Item.
        /// </summary>
        /// <value>The flavor text.</value>
        public string FlavorText
        {
            get { return flavorText; }
        }

        /// <summary>
        /// Gets the Sprite of this Item.
        /// </summary>
        /// <value>The Sprite.</value>
        public Sprite Sprite
        {
            get { return sprite; }
        }

        /// <summary>
        /// Gets the prefab to use when placing the Item in the world.
        /// </summary>
        /// <value>The world drop prefab.</value>
        public GameObject WorldDropPrefab
        {
            get { return worldDropPrefab; }
        }

        /// <summary>
        /// Gets the list of Stat for this Item.
        /// </summary>
        /// <value>The list of Stat.</value>
        public List<Stat> Stats
        { 
            get { return stats; }
        }

        /// <summary>
        /// Gets the EquipmentSlot that this Item occupies.
        /// </summary>
        /// <value>The EquipmentSlot.</value>
        public EquipmentSlot EquipmentSlot
        {
            get { return equipmentSlot; }
        }

        public int Tier
        {
            get { return tier; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:CCC.Items.Item"/> 
        /// struct.
        /// </summary>
        /// <param name="name">The name of the new Item.</param>
        /// <param name="flavorText">The flavor text of the new Item.</param>
        /// <param name="isUnique">
        /// If set to <c>true</c> then the new Item is unique.
        /// </param>
        /// <param name="equipmentSlot">
        /// The EquipmentSlot that the new Item will occupy.
        /// </param>
        /// <param name="sprite">The Sprite that the new Item will use.</param>
        /// <param name="stats">
        /// The list of Stat that the new Item will have.
        /// </param>
        public Item(string name, string flavorText, bool isUnique, int tier,
            EquipmentSlot equipmentSlot, Sprite sprite, List<Stat> stats, 
            GameObject worldDropPrefab)
        {
            this.name = name;
            this.longName = name;
            this.flavorText = flavorText;
            this.isUnique = isUnique;
            this.tier = tier;
            this.equipmentSlot = equipmentSlot;
            this.sprite = sprite;
            this.stats = stats;
            this.worldDropPrefab = worldDropPrefab;
        }

        /// <summary>
        /// The name of this Item.
        /// </summary>
        private string name;


        private string longName;

        /// <summary>
        /// The flavor text of this Item.
        /// </summary>
        private readonly string flavorText;

        /// <summary>
        /// Whether or not this Item is unique.
        /// </summary>
        /// <remarks>
        /// This can be used, for example, to not randomize the names of unique
        /// Items that should always have the same name.
        /// </remarks>
        private bool isUnique;

        private int tier;

        /// <summary>
        /// The Sprite of this Item.
        /// </summary>
        private readonly Sprite sprite;

        /// <summary>
        /// The prefab to use when this Item is placed in the world.
        /// </summary>
        private readonly GameObject worldDropPrefab;

        /// <summary>
        /// The EquipmentSlot that this Item occupies.
        /// </summary>
        private readonly EquipmentSlot equipmentSlot;

        /// <summary>
        /// The list of Stat that this Item has.
        /// </summary>
        private List<Stat> stats;
    }
}
