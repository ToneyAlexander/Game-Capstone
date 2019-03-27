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

        private bool isAvailable = true;

        private bool isDone = false;

        public static SceneChanger Instance
        {
            get { return instance; }
        }

        public bool IsDone
        {
            get { return isDone; }
        }

        public void ChangeToScene(SceneReference sceneReference, 
            UnityAction actionAfterLoad)
        {
            if (true)
            {
                //isAvailable = false;
                //isDone = false;
                //if (sceneReference == null)
                //{
                //    actionAfterLoad();
                //}
                //else
                //{
                    StartCoroutine(CreateAsyncLoad(sceneReference.Path,
                        actionAfterLoad));
                //}
            }
            else
            {
                Debug.Log("SceneChanger is unavailable");
            }
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
            };
            //SceneManager.activeSceneChanged += (Scene current, Scene next) => {
            //    onEnter(game);
            //    Time.timeScale = 1.0f;
            //    isAvailable = true;
            //    isDone = true;
            //};
            SceneManager.activeSceneChanged += lastUsedAction;

            while (!asyncLoad.isDone)
            { 
                if (asyncLoad.progress >= 0.9f)
                {
                    asyncLoad.allowSceneActivation = true;

                    // Unsubscribe the last used UnityAction so that the next 
                    // one doesn't call all the previously called ones.
                    SceneManager.activeSceneChanged -= lastUsedAction;
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