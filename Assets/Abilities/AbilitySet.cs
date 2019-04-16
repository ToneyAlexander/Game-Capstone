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

        private string path;

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

        public void Load()
        {
            Reset();
            path = Path.Combine(Application.persistentDataPath, folderName);
            path = Path.Combine(path, filename);
            var abilityList = AbilityList.CreateEmpty();

            if (File.Exists(path))
            {
                using (StreamReader streamReader = File.OpenText(path))
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

            var directoryPath =
                Path.Combine(Application.persistentDataPath, folderName);

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            using (StreamWriter streamWriter = File.CreateText(path))
            {
                streamWriter.Write(jsonString);
            }

            Reset();
        }

        private void Reset()
        {
            path = "";
            set = new Dictionary<string, Ability>();
        }
    }
}
