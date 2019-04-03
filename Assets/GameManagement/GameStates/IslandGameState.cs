using CCC.Items;
using UnityEngine;

namespace CCC.GameManagement.GameStates
{
    /// <summary>
    /// Represents the state of the game when the player is on a procedurally 
    /// generated island.
    /// </summary>
    [CreateAssetMenu]
    public sealed class IslandGameState : GameState
    {
        /// <summary>
        /// The player's equipment.
        /// </summary>
        [SerializeField]
        private EquipmentDictionary playerEquipment;

        /// <summary>
        /// The player's Inventory.
        /// </summary>
        [SerializeField]
        private Inventory playerInventory;

        public override void Enter()
        {
            Debug.Log("In IslandState.Enter");
            playerInventory.Load();
            playerEquipment.Load();
        }

        public override void Exit()
        {
            Debug.Log("In IslandState.Exit");
            playerInventory.Save();
            playerEquipment.Save();
        }
    }
}
