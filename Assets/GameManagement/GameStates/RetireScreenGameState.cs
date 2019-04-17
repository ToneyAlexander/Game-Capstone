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
        private BloodlineController playerBloodlineController;

        [SerializeField]
        private Inventory inventory;

        [SerializeField]
        private EquipmentDictionary playerEquipment;

        [SerializeField]
        private LevelExpStore playerLevelExp;

        public override void Enter()
        {
            Debug.Log("In RetireScreenState.Enter");
            playerBloodlineController.Load();
            inventory.Load();
            playerEquipment.Load();
            playerLevelExp.Load();
        }

        public override void Exit()
        {
            Debug.Log("In RetireScreenState.Exit");
            playerBloodlineController.Save();
            inventory.Save();
            playerEquipment.Save();
            playerLevelExp.Save();
        }
    }
}
