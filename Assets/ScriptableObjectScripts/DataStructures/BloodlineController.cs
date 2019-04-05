using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[CreateAssetMenu(menuName = "DataStructures/BloodlineController")]
public sealed class BloodlineController : ScriptableObject
{
    [SerializeField]
    private int generationint = 0;
    [SerializeField]
    public int Age { get; private set; }

    [SerializeField]
    public string playerName = "Remy Remmington";

    [SerializeField]
    public ClassPrototype currentClass;

    [SerializeField]
    private List<ClassPrototype> classList = new List<ClassPrototype>();

   // private List<ClassPrototype> pastGenerations = new List<ClassPrototype>();

    
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
        Age = 0;
    }
    public void ageUp()
    {
        Age++;
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
}
