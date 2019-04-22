using UnityEngine;
using System.Collections.Generic;
using System.IO;

namespace CCC.Abilities
{
    /// <summary>
    /// A wrapper around a HashSet of Ability that can be dragged around in the 
    /// Unity inspector.
    /// </summary>
    [CreateAssetMenu(menuName = "Abilities/AbilitySet")]
    public class AbilitySet : ScriptableObject
    {
        [SerializeField]
        private string filename = "NewAbilitySet.json";

        [SerializeField]
        private string folderName = "Player";

        private string filePath;

        private string folderPath;

        /// <summary>
        /// Get the HashSet of Ability that this AbilitySet is a wrapper for.
        /// </summary>
        /// <value>The actual HashSet of Ability.</value>
        public Dictionary<string, Ability> Set
        {
            get { return set; }
        }

        /// <summary>
        /// The actual HashSet of Ability.
        /// </summary>
        private Dictionary<string, Ability> set = new Dictionary<string, Ability>();

        /// <summary>
        /// Delete the JSON file that this AbilitySet saves to.
        /// </summary>
        public void DeleteSaveFile()
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                Debug.Log("[AbilitySet.DeleteSaveFile] Deleted save file");
            }
        }

        public void Load()
        {
            Reset();
            var abilityList = AbilityList.CreateEmpty();

            if (File.Exists(filePath))
            {
                using (StreamReader streamReader = File.OpenText(filePath))
                {
                    var jsonString = streamReader.ReadToEnd();
                    abilityList = JsonUtility.FromJson<AbilityList>(jsonString);
                }
            }

            foreach (var ability in abilityList.Abilities)
            {
                set.Add(ability.AbilityName, ability);
            }
        }

        public void Save()
        {
            var abilities = new List<Ability>();
            foreach (var pair in set)
            {
                // So is always available next game session
                pair.Value.cdRemain = 0.0f;
                pair.Value.use = false;

                abilities.Add(pair.Value);
            }
            var saveData = AbilityList.FromList(abilities);
            var jsonString = JsonUtility.ToJson(saveData, true);

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            using (StreamWriter streamWriter = File.CreateText(filePath))
            {
                streamWriter.Write(jsonString);
            }

            Reset();
        }

        private void Reset()
        {
            set = new Dictionary<string, Ability>();
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
