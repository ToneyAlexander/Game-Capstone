using CCC.Abilities;
using CCC.Combat.Perks;
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
        private AbilitySlotDictionary playerAbilityDictionary;

        [SerializeField]
        private AbilitySet playerAbilitySet;

        [SerializeField]
        private BloodlineController playerBloodlineController;

        [SerializeField]
        private Inventory inventory;

        [SerializeField]
        private EquipmentDictionary playerEquipment;

        [SerializeField]
        private LevelExpStore playerLevelExp;

        [SerializeField]
        private PerkList playerTakenPerks;

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
            inventory.Save();
            playerEquipment.Save();

            playerBloodlineController.DeleteSaveFile();
            playerAbilitySet.DeleteSaveFile();
            playerTakenPerks.DeleteSaveFile();
            playerAbilityDictionary.DeleteSaveFile();
            playerLevelExp.DeleteSaveFile();
        }
    }
}
