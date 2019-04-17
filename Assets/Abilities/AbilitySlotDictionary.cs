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

        private string path;

        private Ability[] abilities;

        [SerializeField]
        private string folderName = "Player";

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
            path = Path.Combine(Application.persistentDataPath, folderName);
            path = Path.Combine(path, filename);

            Reset();
            abilityIconsAssetBundle = 
                AssetBundleManager.LoadAssetBundleAtPath(assetBundlePath);

            if (File.Exists(path))
            {
                using (StreamReader streamReader = File.OpenText(path))
                {
                    var jsonString = streamReader.ReadToEnd();
                    abilities = JsonUtility.FromJson<AbilityList>(jsonString).Abilities;
                }
            }

            Debug.Log(abilities);
        }

        /// <summary>
        /// Delete the JSON file that this AbilitySlotDictionary saves to.
        /// </summary>
        public void DeleteSaveFile()
        {
            if (File.Exists(path))
            {
                File.Delete(path);
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
            using (StreamWriter streamWriter = File.CreateText(path))
            {
                streamWriter.Write(jsonString);
            }
            Debug.Log("[AbilitySlotDictionary.Save] saved to JSON file '" + path + "'");

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
    }
}
