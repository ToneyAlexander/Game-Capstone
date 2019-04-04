using CCC.Items;
using UnityEngine;

namespace CCC.GameManagement.GameStates
{
    /// <summary>
    /// Represents the state when the game is showing the player death screen.
    /// </summary>
    [CreateAssetMenu]
    public sealed class DeathScreenGameState : GameState
    {
        [SerializeField]
        private Inventory inventory;

        [SerializeField]
        private EquipmentDictionary playerEquipment;

        public override void Enter()
        {
            Debug.Log("In DeathScreenState.Enter");
            inventory.Load();
            playerEquipment.Load();
        }

        public override void Exit()
        {
            Debug.Log("In DeathScreenState.Exit");
            inventory.Save();
            playerEquipment.Save();
        }
    }
}
