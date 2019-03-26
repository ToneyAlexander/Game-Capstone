using UnityEngine;

namespace CCC.GameManagement
{
    /// <summary>
    /// Represents a Scene.
    /// </summary>
    [CreateAssetMenu(fileName = "Scene", 
        menuName = "GameManagement/SceneReference")]
    public sealed class SceneReference : ScriptableObject
    {

        /// <summary>
        /// Gets the path to the Scene that this SceneReference represents.
        /// </summary>
        /// <value>The path of the represented Scene.</value>
        public string Path
        {
            get { return path; }
        }

        [SerializeField]
        private string path = "Scenes/";

    }
}
