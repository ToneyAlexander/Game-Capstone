using UnityEngine;

namespace CCC.Items
{
    /// <summary>
    /// Represents a Component that allows its GameObject to have a set of
    /// equipment represented by an EquipmentDictionary.
    /// </summary>
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
    }
}
