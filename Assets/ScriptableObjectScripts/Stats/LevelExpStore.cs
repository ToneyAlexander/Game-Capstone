using CCC.Stats;
using UnityEngine;
using System.IO;

/// <summary>
/// Represents a file on disk (which may or may not exist yet) that stores a 
/// player's level and experience data.
/// </summary>
[CreateAssetMenu(fileName = "NewLevelExpStore")]
public sealed class LevelExpStore : ScriptableObject
{
    /// <summary>
    /// Gets the player's current experience points.
    /// </summary>
    /// <value>The player's current experience points.</value>
    public float Exp
    {
        get { return data.Exp; }
        set { data.Exp = value; }
    }

    /// <summary>
    /// Gets the amount of experience points that the player needs to reach the 
    /// next level.
    /// </summary>
    /// <value>The experience points needed to reach the next level.</value>
    public float ExpToLevel
    {
        get { return data.ExpToLevel; }
        set { data.ExpToLevel = value; }
    }

    /// <summary>
    /// Gets the player's current level.
    /// </summary>
    /// <value>The player's current level.</value>
    public int Level
    {
        get { return data.Level; }
        set { data.Level = value; }
    }

    /// <summary>
    /// Gets the current number of perk points that the player has available to 
    /// use.
    /// </summary>
    /// <value>The number of currently available perk points.</value>
    public int PerkPoints
    {
        get { return data.PerkPoints; }
        set { data.PerkPoints = value; }
    }

    /// <summary>
    /// The actual data this LevelExpStore manages.
    /// </summary>
    private LevelExpData data;

    /// <summary>
    /// The name of the JSON file that this LevelExpStore will load from and 
    /// save to.
    /// </summary>
    [SerializeField]
    private string filename = "NewLevelExpStore.json";

    [SerializeField]
    private string folderName = "Player";

    /// <summary>
    /// The path to the JSON file this LevelExpStore will load from and save 
    /// to.
    /// </summary>
    private string path;

    /// <summary>
    /// Load this LevelExpStore from a JSON file on disk.
    /// </summary>
    public void Load()
    {
        path = Path.Combine(Application.persistentDataPath, folderName);
        path = Path.Combine(path, filename);

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
        else
        {
            // There is no previous file so this new character starts at level 
            // one.
            data = LevelExpData.CreateLevelOne();
        }
    }

    /// <summary>
    /// Save this LevelExpStore to a JSON file on disk.
    /// </summary>
    public void Save()
    {
        string jsonString = JsonUtility.ToJson(new LevelExpData(data.Exp, 
            data.ExpToLevel, data.Level, data.PerkPoints));
        Debug.Log(jsonString);

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

    /// <summary>
    /// Reset this LevelExpStore's LevelExpData so that it can be used to 
    /// reference a different file on disk.
    /// </summary>
    private void Reset()
    {
        data = LevelExpData.CreateEmpty();
    }
}
