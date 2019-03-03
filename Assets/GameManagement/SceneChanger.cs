using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

namespace CCC.GameManagement
{
    /// <summary>
    /// Represents a Component that allows its GameObject to change the current 
    /// Scene.
    /// </summary>
    public sealed class SceneChanger : MonoBehaviour
    {
        /// <summary>
        /// Change to the Scene represented by the given SceneReference.
        /// </summary>
        /// <param name="sceneReference">
        /// The SceneReference that represents the Scene to change to.
        /// </param>
        public void ChangeToScene(SceneReference sceneReference)
        {
            StartCoroutine(CreateSceneAsyncLoad(sceneReference.Path));
        }

        private IEnumerator CreateSceneAsyncLoad(string scenePath)
        {
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scenePath);

            while (!asyncLoad.isDone)
            {
                yield return null;
            }
        }
    }
}