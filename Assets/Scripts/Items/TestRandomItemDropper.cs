using UnityEngine;

using CCC.Stats;

namespace CCC.Items
{
    /// <summary>
    /// Represents a testing Component that causes its GameObject to drop a randomly
    /// generated Item and automatically places in in a given Inventory.
    /// </summary>
    public class TestRandomItemDropper : MonoBehaviour, ItemDropper
    {
        public Item DropItem()
        {
            Item item = itemGenerator.GenerateItem();

            Debug.Log("Dropped " + item.Name + "!");
            foreach (Stat stat in item.Stats)
            {
                Debug.Log(stat.Name + ": " + stat.Value);
            }

            inventory.AddItem(item);

            Debug.Log(inventory.Items[0].Name);

            return item;
        }

        /// <summary>
        /// The ItemGenerator that this TestRandomItemDropper will use to get
        /// generated Items.
        /// </summary>
        [SerializeField]
        private ItemGenerator itemGenerator;

        [SerializeField]
        private Inventory inventory;

        #region MonoBehaviour Messages

        // Just for testing
        private void Start()
        {
            DropItem();
        }
        #endregion
    }
}
