using CCC.Abilities;
using CCC.Combat.Perks;
using CCC.Items;
using CCC.StatManagement;
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
        private AfflictionListStorage playerAfflictionStorage;

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
            Debug.Log("In DeathScreenState.Enter");
            playerBloodlineController.Load();
            inventory.Load();
            playerEquipment.Load();
            playerLevelExp.Load();
            playerAfflictionStorage.Load();
        }

        public override void Exit()
        {
            Debug.Log("In DeathScreenState.Exit");
            inventory.Save();
            playerEquipment.Save();

            playerBloodlineController.DeleteSaveFile();
            playerAbilitySet.DeleteSaveFile();
            playerTakenPerks.DeleteSaveFile();
            playerAbilityDictionary.DeleteSaveFile();
            playerLevelExp.DeleteSaveFile();
            playerAfflictionStorage.DeleteSaveFile();
        }
    }
}
