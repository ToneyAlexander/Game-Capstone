using CCC.GameManagement.GameStates;
using UnityEngine;
using UnityEngine.SceneManagement;

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

        public IGameState currentState = NullGameState.Instance;

        /// <summary>
        /// The SceneChanger to use to change scenes.
        /// </summary>
        private SceneChanger sceneChanger;

        public void TransitionTo(IGameState nextState)
        {
            Time.timeScale = 0;
            currentState.OnExit(this);
            Debug.Log(currentState + " => " + nextState);
            currentState = nextState;
            if (sceneChanger == null)
            {
                sceneChanger = SceneChanger.Instance.GetComponent<SceneChanger>();
            }
            Debug.Log("nextState = " + nextState);
            sceneChanger.ChangeToScene(this, nextState.SceneReference, nextState.OnEnter);
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
    }
}