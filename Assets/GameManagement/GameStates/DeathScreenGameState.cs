using CCC.Abilities;
using CCC.Combat.Perks;
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
        }

        public override void Exit()
        {
            Debug.Log("In DeathScreenState.Exit");
            playerBloodlineController.Save();
            inventory.Save();
            playerEquipment.Save();

            Debug.Log("[DeathScreenGameState.Exit] Before delete AbilitySet");
            playerAbilitySet.DeleteSaveFile();
            Debug.Log("[DeathScreenGameState.Exit] After delete AbilitySet");
            playerTakenPerks.DeleteSaveFile();
            playerAbilityDictionary.DeleteSaveFile();
            playerLevelExp.DeleteSaveFile();
        }
    }
}
