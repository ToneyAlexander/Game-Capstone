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
        private string abilityIconAssetBundlePath = "Assets/AssetBundles/";

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
            path = Path.Combine(Application.persistentDataPath, filename);
            Reset();
            abilityIconsAssetBundle = 
                AssetBundleManager.LoadAssetBundleAtPath(abilityIconAssetBundlePath);

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

        public void Save()
        {
            foreach (var ability in abilities)
            {
                ability.cdRemain = 0.0f;
                ability.use = false;
            }

            var saveData = AbilityList.FromArray(abilities);

            var jsonString = JsonUtility.ToJson(saveData, true);
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
            AssetBundleManager.UnloadAssetBundleAtPath(abilityIconAssetBundlePath);
            abilityIconsAssetBundle = null;
        }
    }
}
