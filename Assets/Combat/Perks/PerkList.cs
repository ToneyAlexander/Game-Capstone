using UnityEngine;
using System.Collections.Generic;
using System.IO;

namespace CCC.Combat.Perks
{
    /// <summary>
    /// References an array of perks stored on disk.
    /// </summary>
    [CreateAssetMenu(menuName = "Combat/Perks/PerkList")]
    public sealed class PerkList : ScriptableObject
    {
        private PerkListData data = PerkListData.Null;

        public List<PerkPrototype> Perks
        {
            get
            {
                List<PerkPrototype> perks = data.Perks;

                if (data == PerkListData.Null)
                {
                    perks = new List<PerkPrototype>();
                }

                return perks;
            }
        }

        [SerializeField]
        private string description; // Only for viewing in the Inspector

        [SerializeField]
        private string filename;

        [SerializeField]
        private string folderName = "Player";

        private string path;

        public void AddPerk(PerkPrototype p)
        {
            if (filename != "")
            {
                data.Perks.Add(p);
            }
        }

        /// <summary>
        /// Delete the JSON file that this PerkList saves to.
        /// </summary>
        public void DeleteSaveFile()
        {
            if (File.Exists(path))
            {
                File.Delete(path);
                Debug.Log("[PerkList.DeleteSaveFile] Deleted save file");
            }
        }

        public void Load()
        {
            Debug.Log("Loading");
            if (filename != "")
            {
                path = Path.Combine(Application.persistentDataPath, folderName);
                path = Path.Combine(path, filename);

                if (File.Exists(path))
                {
                    using (StreamReader streamReader = File.OpenText(path))
                    {
                        var jsonString = streamReader.ReadToEnd();
                        Debug.Log(jsonString);
                        var loadedData =
                            JsonUtility.FromJson<PerkListData>(jsonString);
                        Debug.Log(loadedData.Perks.Count);
                        data = loadedData;

                        foreach (var perk in loadedData.Perks)
                        {
                            Debug.Log("[" + name + "PerkList.Load] perk = " + perk);
                        }
                    }
                }
                else
                {
                    // We're creating the data for the first time
                    data = PerkListData.Empty();
                }
            }
        }

        public void Save(List<PerkPrototype> perkPrototypes)
        {
            var newData = PerkListData.ForPerkPrototypes(perkPrototypes);

            if (filename != "")
            {
                foreach (var perk in perkPrototypes)
                {
                    Debug.Log("[" + name + "PerkList.Save] perk = " + perk);
                }

                var jsonString = JsonUtility.ToJson(newData, true);

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
            }

            data = PerkListData.Null;
        }
    }
}