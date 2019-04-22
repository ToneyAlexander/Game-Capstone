using CCC.Stats;
using UnityEngine;
using System.Collections.Generic;
using System.IO;

namespace CCC.StatManagement
{
    /// <summary>
    /// A reference to a JSON file that contains Afflictions.
    /// </summary>
    [CreateAssetMenu(menuName = "StatManagement/AfflictionListStorage", 
        fileName = "NewAfflictionListStroage")]
    sealed class AfflictionListStorage : ScriptableObject
    {
        public List<Affliction> Afflictions
        {
            get { return afflictions; }
            set { afflictions = value; }
        }

        private List<Affliction> afflictions;

        /// <summary>
        /// The name of the JSON file that contains the data for this 
        /// AfflictionListStorage.
        /// </summary>
        [SerializeField]
        private string filename = "NewAfflictionList.json";

        /// <summary>
        /// The name of the folder that contains the JSON file for this 
        /// AfflictionListStorage.
        /// </summary>
        [SerializeField]
        private string folderName = "Player";

        /// <summary>
        /// The path to the JSON file for this AfflictionListStorage.
        /// </summary>
        /// <remarks>
        /// Used to save and load the data from the JSON file and create it if 
        /// it doesn't exist.
        /// </remarks>
        private string filePath;

        /// <summary>
        /// The path to the folder that the JSON file for this 
        /// AfflictionListStorage is saved in.
        /// </summary>
        /// <remarks>Used to create the directory if it doesn't exist.</remarks>
        private string folderPath;

        /// <summary>
        /// Delete the JSON file that stores the data for this 
        /// AfflictionListStorage.
        /// </summary>
        public void DeleteSaveFile()
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

        /// <summary>
        /// Load the JSON data for this AfflictionDataStorage.
        /// </summary>
        public void Load()
        {
            Reset();

            if (File.Exists(filePath))
            {
                using (var streamReader = File.OpenText(filePath))
                {
                    var jsonString = streamReader.ReadToEnd();
                    var data = JsonUtility.FromJson<AfflictionListData>(jsonString);
                    afflictions = new List<Affliction>(data.Afflictions);
                }
            }
        }

        /// <summary>
        /// Save the data for this AfflictionListStorage to a JSON file.
        /// </summary>
        public void Save()
        {
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            var data = AfflictionListData.FromList(afflictions);
            var jsonString = JsonUtility.ToJson(data);
            using (var streamWriter = File.CreateText(filePath))
            {
                streamWriter.Write(jsonString);
            }

            Reset();
        }

        private void Reset()
        {
            afflictions = new List<Affliction>();
        }

        #region ScriptableObject Messages
        private void OnEnable()
        {
            Reset();
            folderPath = 
                Path.Combine(Application.persistentDataPath, folderName);
            filePath = Path.Combine(folderPath, filename);
        }
        #endregion
    }
}