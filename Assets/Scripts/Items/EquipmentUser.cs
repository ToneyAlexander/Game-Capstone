using UnityEngine;

namespace CCC.Items
{
    /// <summary>
    /// Represents a Component that allows its GameObject to have a set of
    /// equipment represented by an EquipmentDictionary.
    /// </summary>
    [RequireComponent(typeof(ControlStatBlock))]
    public class EquipmentUser : MonoBehaviour
    {
        /// <summary>
        /// Gets the EquipmentDictionary that represents the equipment of this
        /// Component's GameObject.
        /// </summary>
        /// <value>The EquipmentDictionary.</value>
        public EquipmentDictionary Equipment
        {
            get { return equipment; }
        }

        /// <summary>
        /// The EquipmentDictionary.
        /// </summary>
        [SerializeField]
        private EquipmentDictionary equipment;

        private ControlStatBlock controlStatBlock;

        private void Update()
        {
            // TODO Put in on equip
            controlStatBlock.StatsChanged();
        }

        private void Start()
        {
            controlStatBlock = GetComponent<ControlStatBlock>();
        }
    }
}
