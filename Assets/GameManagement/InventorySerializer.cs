using CCC.Items;
using UnityEngine;
using System.Collections.Generic;
using System.IO;

namespace CCC.GameManagement
{
    public static class InventorySerializer
    {
        public static string ToJson(Inventory inventory)
        {
            string json = "";

            string path = inventory.Path;
            if (File.Exists(path))
            {
                using (StreamReader streamReader = File.OpenText(path))
                {
                    string jsonString = streamReader.ReadToEnd();
                    Debug.Log("jsonString = " + jsonString);
                    List<Item> items = 
                        JsonUtility.FromJson<List<Item>>(jsonString);
                }
            }

            return json;
        }
    }
}