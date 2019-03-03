using CCC.GameManagement.GameStates;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

namespace CCC.GameManagement
{
    /// <summary>
    /// Represents the Game as a whole and talks to all other subsystems.
    /// </summary>
    [CreateAssetMenu(fileName = "GameManagement", 
        menuName = "GameManagement/Game")]
    public sealed class Game : ScriptableObject
    {
        /// <summary>
        /// The currently active Scene.
        /// </summary>
        private Scene currentScene;

        private IGameState currentState = NullGameState.Instance;

        public IGameState CurrentState
        {
            get { return currentState; }
            set
            {
                currentState.Exit(this);
                currentState = value;
                currentState.Enter(this);
            }
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

        IEnumerator LoadSceneAsync(string path)
        {
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(path);

            while (!asyncLoad.isDone)
            {
                yield return null;
            }
        }

#region ScriptableObject Messages
        private void OnEnable()
        {
            currentScene = SceneManager.GetActiveScene();
        }
#endregion
    }
}