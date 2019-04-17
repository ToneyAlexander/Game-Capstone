using UnityEngine;

namespace CCC.GameManagement.GameStates
{
    /// <summary>
    /// Represents the state when the game is waiting for the player to select 
    /// their class.
    /// </summary>
    [CreateAssetMenu]
    public sealed class ClassSelectionGameState : GameState
    {
        [SerializeField]
        private BloodlineController bloodline;

        public override void Enter()
        {
            Debug.Log("In ClassSelectionState.Enter");
            //Do bloodline stuff
            string GivenName = bloodline.GenerateGivenName();
            string FamilyName = bloodline.GetOrCreateFamilyName();
            bloodline.playerName = GivenName + " " + FamilyName;
        }

        public override void Exit()
        {
            Debug.Log("In ClassSelectionState.Exit");
        }
    }
}
