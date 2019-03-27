using CCC.GameManagement.GameStates;
using UnityEngine;

namespace CCC.GameManagement
{
    /// <summary>
    /// Manages a game session and its overall states.
    /// </summary>
    public sealed class GameManager : MonoBehaviour
    {
        /// <summary>
        /// The single GameManager instance that can exist.
        /// </summary>
        private static GameManager instance;

        /// <summary>
        /// Gets the GameManager singleton.
        /// </summary>
        /// <value>The GameManager singleton.</value>
        public static GameManager Instance
        {
            get { return instance; }
        }

        /// <summary>
        /// The SceneChanger that will be used to change the current scene.
        /// </summary>
        private SceneChanger sceneChanger;

        /// <summary>
        /// The current GameState that the game is in.
        /// </summary>
        [SerializeField]
        private GameState currentState;

        /// <summary>
        /// Transition the game from its current state to the given GameState.
        /// </summary>
        /// <param name="nextState">
        /// The GameState to transition the game to.
        /// </param>
        public void TransitionTo(GameState nextState)
        {
            currentState.Exit();
            currentState = nextState;
            currentState.Enter();
        }

        /// <summary>
        /// Quit this Game.
        /// </summary>
        public void Quit()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

        #region MonoBehaviour Messages
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;

                // We can call Enter on the very first state since GameManager will 
                // only ever have one instance that has Awake called on it.
                currentState.Enter();
            }
            else
            {
                Destroy(gameObject);
            }

            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            // In Start because SceneChanger creates itself in its Awake
            sceneChanger = SceneChanger.Instance;
        }
        #endregion
    }
}
