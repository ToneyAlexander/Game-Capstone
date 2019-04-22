using CCC.Abilities;
using CCC.Combat.Perks;
using CCC.Items;
using CCC.StatManagement;
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
        [SerializeField]
        private AfflictionListStorage playerAfflictionStorage;

        [SerializeField]
        private BloodlineController playerBloodlineController;

        /// <summary>
        /// The player's equipment.
        /// </summary>
        [SerializeField]
        private EquipmentDictionary playerEquipment = null;

        /// <summary>
        /// The player's Inventory.
        /// </summary>
        [SerializeField]
        private Inventory playerInventory = null;

        /// <summary>
        /// The player's current level and experience.
        /// </summary>
        [SerializeField]
        private LevelExpStore playerLevelExpStore = null;

        /// <summary>
        /// The player's currently taken perks.
        /// </summary>
        [SerializeField]
        private PerkList playerTakenPerks = null;

        /// <summary>
        /// The PlayerClass Component of the player.
        /// Needed so the list of PerkPrototype that was held by this 
        /// PlayerClass during run-time can be saved.
        /// </summary>
        private PlayerClass playerPlayerClass;

        [SerializeField]
        private AbilitySlotDictionary playerAbilitySlots = null;

        [SerializeField]
        private AbilitySet playerAbilitySet = null;

        public override void Enter()
        {
            Debug.Log("In IslandState.Enter");
            playerBloodlineController.Load();
            playerInventory.Load();
            playerEquipment.Load();
            playerLevelExpStore.Load();
            playerAbilitySet.Load();
            playerAbilitySlots.Load();
            playerAfflictionStorage.Load();
        }

        public override void Exit()
        {
            Debug.Log("In IslandState.Exit");
            AgePlayer();
            playerBloodlineController.Save();
            playerInventory.Save();
            playerEquipment.Save();
            playerLevelExpStore.Save();
            SavePlayerPerks();
            playerAbilitySet.Save();
            playerAbilitySlots.Save();

            AfflictPlayer();
            playerAfflictionStorage.Save();
        }

        private void AgePlayer()
        {
            var playerClass = FindPlayerPlayerClass();

            if (playerClass)
            {
                playerClass.IncreaseAge();
            }
            else
            {
                Debug.LogError("[IslandGameState.Exit.AgePlayer] No " + 
                    "GameObject with tag 'Player' found!");
            }
        }

        private void AfflictPlayer()
        {
            var controlStatBlock = FindPlayerControlStatBlock();

            if (controlStatBlock)
            {
                playerAfflictionStorage.Afflictions = 
                    controlStatBlock.afflictions;
                Debug.Log("[IslandGameState.AfflictPlayer] controlStatBlock.afflictions = " + controlStatBlock.afflictions);
            }
            else
            {
                Debug.LogError("[IslandGameState.Exit.AfflictPlayer] No " +
                    "GameObject with tag 'Player' found!");
            }
        }

        private PlayerClass FindPlayerPlayerClass()
        {
            return GameObject.FindWithTag("Player").GetComponent<PlayerClass>();
        }

        private ControlStatBlock FindPlayerControlStatBlock()
        {
            return GameObject.FindWithTag("Player").GetComponent<ControlStatBlock>();
        }

        /// <summary>
        /// Save the player's currently taken perks.
        /// This must be done here because a PlayerClass Component stores the 
        /// actual list of 
        /// </summary>
        private void SavePlayerPerks()
        {
            var playerClass = FindPlayerPlayerClass();

            if (playerClass != null)
            {
                playerTakenPerks.Save(playerClass.TakenPerks);
            }
            else
            {
                Debug.LogError("[IslandGameState.Exit.SavePlayerPerks] No " +
                    "GameObject with tag 'Player' found!");
            }
        }
    }
}
