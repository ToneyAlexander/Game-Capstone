﻿using CCC.Items;
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
        private Inventory playerInventory;

        public override void Enter()
        {
            Debug.Log("In IslandState.Enter");
            playerInventory.Load();
        }

        public override void Exit()
        {
            Debug.Log("In IslandState.Exit");
            playerInventory.Save();
        }
    }
}