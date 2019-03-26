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
        private static SceneChanger instance;

        public static SceneChanger Instance
        {
            get { return instance; }
        }

        /// <summary>
        /// Change to the Scene represented by the given SceneReference.
        /// </summary>
        /// <param name="sceneReference">
        /// The SceneReference that represents the Scene to change to.
        /// </param>
        public void ChangeToScene(Game game, SceneReference sceneReference)
        {
            if (sceneReference != null)
            {
                StartCoroutine(CreateAsyncLoad(game, sceneReference.Path,
                    (Game g) => { }));
            }
        }

        public void ChangeToScene(Game game, SceneReference sceneReference, 
            UnityAction<Game> actionAfterLoad)
        {
            if (sceneReference == null)
            {
                actionAfterLoad(game);
            }
            else
            {
                StartCoroutine(CreateAsyncLoad(game, sceneReference.Path,
                    actionAfterLoad));
            }
        }

        private IEnumerator CreateAsyncLoad(Game game, string scenePath, UnityAction<Game> onEnter)
        {
            yield return null;  // Wait one frame

            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scenePath);
            asyncLoad.allowSceneActivation = false;
            SceneManager.activeSceneChanged += (Scene current, Scene next) => {
                onEnter(game);
                Time.timeScale = 1.0f;
            };

            while (!asyncLoad.isDone)
            { 
                if (asyncLoad.progress >= 0.9f)
                {
                    asyncLoad.allowSceneActivation = true;
                }
                yield return null;
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