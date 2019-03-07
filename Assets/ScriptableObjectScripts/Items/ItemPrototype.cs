using System.Collections.Generic;
using UnityEngine;

using CCC.Stats;

namespace CCC.Items
{
    /// <summary>
    /// Represents the information that an Item can base its randomly generated
    /// values on.
    /// </summary>
    [CreateAssetMenu(menuName = "Items/ItemPrototype")]
    public sealed class ItemPrototype : ScriptableObject
    {
        /// <summary>
        /// Gets the EquipmentSlot that Items based off of this ItemPrototype
        /// will always have.
        /// </summary>
        /// <value>The EquipmentSlot.</value>
        public EquipmentSlot EquipmentSlot
        {
            get { return equipmentSlot; }
        }

        /// <summary>
        /// Gets the flavor text that Items based off of this ItemPrototype can
        /// have.
        /// </summary>
        /// <value>The flavor text.</value>
        public string FlavorText
        {
            get { return flavorText; }
        }

        /// <summary>
        /// Gets the name that Items based off of this ItemPrototype can have.
        /// </summary>
        /// <value>The item name.</value>
        public string ItemName
        {
            get { return itemName; }
        }

        /// <summary>
        /// Gets the Sprite that Items based off of this ItemPrototype will
        /// always have.
        /// </summary>
        /// <value>The Sprite.</value>
        public Sprite Sprite
        {
            get { return sprite; }
        }

        public int BaseItemTier
        {
            get { return baseItemTier; }
        }

        /// <summary>
        /// Gets the maximum number of affixes that an Item based off of this 
        /// ItemPrototype can have.
        /// </summary>
        /// <value>The maximum number of affixes.</value>
        public int MaxNumberAffixes
        {
            get { return maxNumberAffixes; }
        }

        /// <summary>
        /// Gets a list of StatPrototype that define are used to create the
        /// Stats that Items based off of this ItemPrototype will have.
        /// </summary>
        /// <value>The list of StatPrototype.</value>
        public List<StatPrototype> StatPrototypes
        {
            get { return statPrototypes; }
        }

        /// <summary>
        /// The name that Items based off of this ItemPrototype will have.
        /// </summary>
        [SerializeField]
        private string itemName = "";

        /// <summary>
        /// The Sprite that Items based off of this ItemPrototype will have.
        /// </summary>
        [SerializeField]
        private Sprite sprite;

        /// <summary>
        /// The EquipmentSlot that Items based off of this ItemPrototype will
        /// have.
        /// </summary>
        [SerializeField]
        private EquipmentSlot equipmentSlot = EquipmentSlot.Null;

        /// <summary>
        /// The flavor text that Items based off of this ItemPrototype will 
        /// have.
        /// </summary>
        [SerializeField]
        private string flavorText = "";

        /// <summary>
        /// The internal description of this ItemPrototype. This will not be
        /// passed on to any Items created from this ItemPrototype and is used
        /// only to provide a description in the Unity Inspector.
        /// </summary>
        [SerializeField]
        private string internalDescription = "";


        [SerializeField]
        private int baseItemTier;

        /// <summary>
        /// The maximum number of affixes that Items generated from this 
        /// ItemPrototype can possibly have.
        /// </summary>
        [SerializeField]
        private int maxNumberAffixes;

        /// <summary>
        /// The list of StatPrototyeSlotEntry used by this ItemPrototype. All
        /// StatPrototypeSlotEntry are created in the Unity Inspector on the
        /// ItemPrototype that they belong to. Each of them will be turned into
        /// an equivalent StatPrototype which will be available to other 
        /// objects.
        /// </summary>
        [SerializeField]
        private List<StatPrototypeSlotEntry> statPrototypeList = new List<StatPrototypeSlotEntry>();


        /// <summary>
        /// The StatPrototypes that will be used to create Stats for Items
        /// based off of this ItemPrototype.
        /// </summary>
        private List<StatPrototype> statPrototypes = new List<StatPrototype>();

        #region ScriptableObject Messages
        private void OnEnable()
        {
            statPrototypes = new List<StatPrototype>();
            foreach (StatPrototypeSlotEntry entry in statPrototypeList)
            {
                statPrototypes.Add(new StatPrototype(entry.StatName, 
                    entry.MinValue, entry.MaxValue));
            }
        }
        #endregion
    }
}
