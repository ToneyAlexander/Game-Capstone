using CCC.Stats;
using UnityEngine;
using System.IO;

[CreateAssetMenu]
public sealed class LevelExpStore : ScriptableObject
{
    public float Exp
    {
        get { return data.Exp; }
        set { data.Exp = value; }
    }

    public float ExpToLevel
    {
        get { return data.ExpToLevel; }
        set { data.ExpToLevel = value; }
    }

    public int Level
    {
        get { return data.Level; }
        set { data.Level = value; }
    }

    public int PerkPoints
    {
        get { return data.PerkPoints; }
        set { data.PerkPoints = value; }
    }

    [SerializeField]
    private string filename;

    private string path;

    private LevelExpData data = null;

    public void Load()
    {
        if (File.Exists(path))
        {
            using (StreamReader streamReader = File.OpenText(path))
            {
                string jsonString = streamReader.ReadToEnd();
                LevelExpData loadedData = 
                    JsonUtility.FromJson<LevelExpData>(jsonString);
                data = loadedData;
            }
        }
    }

    public void Save()
    {
        string jsonString = JsonUtility.ToJson(new LevelExpData(data.Exp, 
            data.ExpToLevel, data.Level, data.PerkPoints));
        Debug.Log(jsonString);

        using (StreamWriter streamWriter = File.CreateText(path))
        {
            streamWriter.Write(jsonString);
        }
    }

    #region ScriptableObject Messages
    private void OnEnable()
    {
        path = Path.Combine(Application.persistentDataPath, filename);
    }
    #endregion
}
