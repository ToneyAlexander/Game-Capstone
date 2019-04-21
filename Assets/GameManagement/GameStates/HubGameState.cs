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

        [SerializeField]
        private LevelExpStore playerLevelExp;

        public override void Enter()
        {
            Debug.Log("In HubState.Enter");
            playerLevelExp.Load();
            crewController.recruited = false;
            BoatCameraController.moving = false;
        }

        public override void Exit()
        {
            Debug.Log("In HubState.Exit");
            playerLevelExp.Save();
        }
    }
}
