using CCC.Stats;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[CreateAssetMenu(menuName = "DataStructures/BloodlineController")]
public sealed class BloodlineController : ScriptableObject
{
    [SerializeField]
    private int generationint = 0;

    public int Age
    {
        get { return age; }
    }

    public ClassPrototype CurrentClass
    {
        get { return currentClass; }
        set { currentClass = value; }
    }

    private int age;

    private string folderPath;
    private string filePath;

    [SerializeField]
    public string playerName = "Remy Remmington";

    [SerializeField]
    private ClassPrototype currentClass;

    [SerializeField]
    private string filename = "NewBloodlineController.json";

    [SerializeField]
    private string folderName = "Player";

    [SerializeField]
    private List<ClassPrototype> classList = new List<ClassPrototype>();

    private List<string> pastNames = new List<string>();

    public void addGeneration()
    {
        pastNames.Add(playerName + " the " + currentClass.name);
    }
    public void endGeneration(bool retired)
    {
        if (retired)
        {
            pastNames[pastNames.Count - 1] = playerName + " the " + currentClass.name + "\n" + "Retired at" + Age;
        }
        else
        {
            pastNames[pastNames.Count - 1] = playerName + " the " + currentClass.name + "\n" + "Died at" + Age;
        }

        age = 0;
    }

    public void ageUp()
    {
        age++;
    }

    public List<ClassPrototype> ClassList
    {
        get
        {
            return classList;
        }
        set
        {
            classList = value;
        }
    }

    private string SelectLineFromFile(TextAsset File)
    {
        string[] lines = File.text.Split('\n');

        for (int i = 0; i < lines.Length; i++)
        {
            lines[i] = lines[i].Replace("\r", "");
        }

        return lines[Random.Range(0, lines.Length)];
    }

    public string GenerateGivenName()
    {
        return SelectLineFromFile(Resources.Load<TextAsset>("firstNames"));
    }
    public string GenerateFamilyName()
    {
        return SelectLineFromFile(Resources.Load<TextAsset>("lastNames"));
    }
    public string GetOrCreateFamilyName()
    {
        string path = System.IO.Path.Combine(Application.persistentDataPath,
            "FamilyName.txt");

        string lastName = "";
        if (File.Exists(path))
        {
            using (StreamReader streamReader = File.OpenText(path))
            {
                lastName = streamReader.ReadLine().Trim();
            }
        }
        else
        {
            using (StreamWriter streamWriter = File.CreateText(path))
            {
                lastName = GenerateFamilyName();
                streamWriter.Write(lastName);
            }
        }
        return lastName;
    }
    public void GenerateNewFamilyName()
    {
        string path = 
            Path.Combine(Application.persistentDataPath, "FamilyName.txt");

        if (File.Exists(path))
        {
            File.Delete(path);
        }

        GetOrCreateFamilyName();
    }

    /// <summary>
    /// Delete the JSON file that this BloodlineController saves to.
    /// </summary>
    public void DeleteSaveFile()
    {
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
    }

    public void Load()
    {
        Reset();

        if (File.Exists(filePath))
        {
            using (var streamReader = File.OpenText(filePath))
            {
                var jsonString = streamReader.ReadToEnd();
                var data = JsonUtility.FromJson<BloodlineData>(jsonString);
                age = data.Age;
            }
        }
    }

    public void Save()
    {
        var data = BloodlineData.ForAge(age);
        var jsonString = JsonUtility.ToJson(data, true);

        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        using (var streamWriter = File.CreateText(filePath))
        {
            streamWriter.Write(jsonString);
        }

        Reset();
    }

    private void Reset()
    {
        folderPath =
            Path.Combine(Application.persistentDataPath, folderName);
        filePath = Path.Combine(folderPath, filename);
        age = 0;
    }
}
