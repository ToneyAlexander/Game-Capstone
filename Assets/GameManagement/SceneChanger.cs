using UnityEngine;
using UnityEngine.Events;
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
        /// Gets the singleton SceneChanger instance.
        /// </summary>
        /// <value>The instance.</value>
        public static SceneChanger Instance
        {
            get { return instance; }
        }

        /// <summary>
        /// The singleton SceneChanger instance.
        /// </summary>
        private static SceneChanger instance;

        /// <summary>
        /// Change to the Scene represented by the given SceneReference and 
        /// call the given UnityAction after the new Scene is loaded.
        /// </summary>
        /// <param name="sceneReference">
        /// The SceneReference that represents the new Scene to load.
        /// </param>
        /// <param name="actionAfterLoad">
        /// The UnityAction that should be called after the new Scene is 
        /// loaded. This UnityAction will be executed before the Start method 
        /// of any Component in the Scene that is loaded.
        /// </param>
        public void ChangeToScene(SceneReference sceneReference, 
            UnityAction actionAfterLoad)
        {
            StartCoroutine(CreateAsyncLoad(sceneReference.Path, 
                actionAfterLoad));
        }

        private UnityAction<Scene, Scene> lastUsedAction = 
            (Scene current, Scene next) => { };

        private IEnumerator CreateAsyncLoad(string scenePath, 
            UnityAction onEnter)
        {
            yield return null;  // Wait one frame

            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scenePath);
            asyncLoad.allowSceneActivation = false;
            lastUsedAction = (Scene current, Scene next) => {
                onEnter();
                // Unsubscribe the last used UnityAction so that the next 
                // one doesn't call all the previously called ones.
                SceneManager.activeSceneChanged -= lastUsedAction;
            };
            SceneManager.activeSceneChanged += lastUsedAction;

            while (!asyncLoad.isDone)
            { 
                if (asyncLoad.progress >= 0.9f)
                {
                    asyncLoad.allowSceneActivation = true;
                }
                yield return null;  // Wait one frame
            }
        }

        #region MonoBehaviour Messages
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                // Only allow there to be one instance of SceneChanger at a time
                Destroy(gameObject);
            }

            DontDestroyOnLoad(gameObject);
        }
        #endregion
    }
}