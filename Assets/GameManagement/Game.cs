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

        /// <summary>
        /// Change to the Scene represented by the given SceneReference.
        /// </summary>
        /// <param name="sceneReference">The SceneReference.</param>
        public void ChangeToScene(SceneReference sceneReference)
        {
            LoadSceneAsync(sceneReference.Path);
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