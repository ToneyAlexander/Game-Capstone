using CCC.Items;
using UnityEngine;

namespace CCC.GameManagement.GameStates
{
    /// <summary>
    /// Represents the state when the game is showing the player retire screen.
    /// </summary>
    [CreateAssetMenu]
    public sealed class RetireScreenGameState : GameState
    {
        [SerializeField]
        private Inventory inventory;

        [SerializeField]
        private EquipmentDictionary playerEquipment;

        public override void Enter()
        {
            Debug.Log("In RetireScreenState.Enter");
            inventory.Load();
            playerEquipment.Load();
        }

        public override void Exit()
        {
            Debug.Log("In RetireScreenState.Exit");
            inventory.Save();
            playerEquipment.Save();
        }
    }
}
