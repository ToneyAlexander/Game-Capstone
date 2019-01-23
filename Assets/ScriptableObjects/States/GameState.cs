using UnityEngine;
using UnityEngine.SceneManagement;

namespace States
{
    /// <summary>
    /// Represents a state of the game. All GameStates load a Scene that
    /// corresponds to the state.
    /// </summary>
    [CreateAssetMenu]
    public sealed class GameState : State
    {
        [SerializeField]
        private string description;

        [SerializeField]
        private string sceneName;

        public override void Handle()
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
