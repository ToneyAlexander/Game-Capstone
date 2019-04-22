using CCC.AssetManagement;
using UnityEngine;
using System;
using System.IO;

namespace CCC.Abilities
{
    [CreateAssetMenu(fileName = "NewAbilitySlotDictionary", 
        menuName = "Abilities/AbilitySlotDictionary")]
    public sealed class AbilitySlotDictionary : ScriptableObject
    {
        [SerializeField]
        private string filename = "NewAbilitySlotDictionary.json";

        private Ability[] abilities;

        [SerializeField]
        private string folderName = "Player";

        private string filePath;

        private string folderPath;

        [SerializeField]
        private string assetBundleName = "ability_icons";

        private string assetBundlePath;

        public Ability[] Abilities
        {
            get { return abilities; }
        }

        public AssetBundle AbilityIconsAssetBundle
        {
            get { return abilityIconsAssetBundle; }
        }

        private AssetBundle abilityIconsAssetBundle = null;

        public Ability GetAbility(AbilitySlot slot)
        {
            return abilities[((int)slot) - 1];
        }

        public void SetSlotAbility(AbilitySlot slot, Ability ability)
        {
            abilities[((int)slot) - 1] = ability;
        }

        public void Load()
        {
            Reset();

            abilityIconsAssetBundle = 
                AssetBundleManager.LoadAssetBundleAtPath(assetBundlePath);

            if (File.Exists(filePath))
            {
                using (StreamReader streamReader = File.OpenText(filePath))
                {
                    var jsonString = streamReader.ReadToEnd();
                    abilities = JsonUtility.FromJson<AbilityList>(jsonString).Abilities;
                }
            }
        }

        /// <summary>
        /// Delete the JSON file that this AbilitySlotDictionary saves to.
        /// </summary>
        public void DeleteSaveFile()
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

        public void Save()
        {
            foreach (var ability in abilities)
            {
                ability.cdRemain = 0.0f;
                ability.use = false;
            }

            var saveData = AbilityList.FromArray(abilities);

            var jsonString = JsonUtility.ToJson(saveData, true);
            var directoryPath =
                Path.Combine(Application.persistentDataPath, folderName);

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            Debug.Log(jsonString);
            using (StreamWriter streamWriter = File.CreateText(filePath))
            {
                streamWriter.Write(jsonString);
            }
            Debug.Log("[AbilitySlotDictionary.Save] saved to JSON file '" + filePath + "'");

            Reset();
        }

        private void Reset()
        {
            abilities = new Ability[Enum.GetNames(typeof(AbilitySlot)).Length - 1];
            for (int i = 0; i < abilities.Length; i++)
            {
                abilities[i] = Ability.Null;
            }

            assetBundlePath = Path.Combine(Application.persistentDataPath, "AssetBundles");
            assetBundlePath = Path.Combine(assetBundlePath, assetBundleName);

            AssetBundleManager.UnloadAssetBundleAtPath(assetBundlePath);
            abilityIconsAssetBundle = null;
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
