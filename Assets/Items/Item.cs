﻿using CCC.Stats;
using UnityEngine;
using System.Collections.Generic;

namespace CCC.Items
{
    /// <summary>
    /// Represents an item in the game.
    /// </summary>
    [System.Serializable]
    public sealed class Item
    {
        public static Item FromData()
        {
            return null;
        }

        /// <summary>
        /// The null Item that is used when no other Item makes sense.
        /// </summary>
        public static Item Null = new Item(
            " ", 
            "--",
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

        public string SpriteName
        {
            get { return spriteName; }
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
            EquipmentSlot equipmentSlot, string spriteName, List<Stat> stats, 
            GameObject worldDropPrefab)
        {
            this.name = name;
            this.longName = name;
            this.flavorText = flavorText;
            this.isUnique = isUnique;
            this.tier = tier;
            this.equipmentSlot = equipmentSlot;
            this.stats = stats;
            this.worldDropPrefab = worldDropPrefab;
            this.spriteName = spriteName;
        }

        /// <summary>
        /// The name of this Item.
        /// </summary>
        [SerializeField]
        private string name;

        [SerializeField]
        private string longName;

        /// <summary>
        /// The flavor text of this Item.
        /// </summary>
        [SerializeField]
        private string flavorText;

        /// <summary>
        /// Whether or not this Item is unique.
        /// </summary>
        /// <remarks>
        /// This can be used, for example, to not randomize the names of unique
        /// Items that should always have the same name.
        /// </remarks>
        [SerializeField]
        private bool isUnique;

        [SerializeField]
        private int tier;

        /// <summary>
        /// The prefab to use when this Item is placed in the world.
        /// </summary>
        [SerializeField]
        private GameObject worldDropPrefab;

        /// <summary>
        /// The EquipmentSlot that this Item occupies.
        /// </summary>
        [SerializeField]
        private EquipmentSlot equipmentSlot;

        /// <summary>
        /// The list of Stat that this Item has.
        /// </summary>
        [SerializeField]
        private List<Stat> stats;

        [SerializeField]
        private string spriteName;
    }
}
