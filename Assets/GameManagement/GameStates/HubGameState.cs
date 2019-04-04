using UnityEngine;

namespace CCC.GameManagement.GameStates
{
    /// <summary>
    /// Represents the state when the game is at the hub scene and waiting for 
    /// the player to select an island to go to.
    /// </summary>
    /// 

    [CreateAssetMenu]
    public sealed class HubGameState : GameState
    {
        [SerializeField]
        private CrewController crewController;

        public override void Enter()
        {
            Debug.Log("In HubState.Enter");
            crewController.recruited = false;
        }

        public override void Exit()
        {
            Debug.Log("In HubState.Exit");
        }
    }
}
