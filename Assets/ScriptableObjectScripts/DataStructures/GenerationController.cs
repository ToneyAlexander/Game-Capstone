using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerationController : ScriptableObject
{
    [SerializeField]
    private int generationint = 0;

    [SerializeField]
    private string name = "Remy";

    [SerializeField]
    private ClassPrototype currentClass;

    [SerializeField]
    private List<ClassPrototype> classList = new List<ClassPrototype>();

   // private List<ClassPrototype> pastGenerations = new List<ClassPrototype>();
    private List<string> pastNames = new List<string>();

    void addGeneration()
    {
        pastNames.Add(name + " the " + currentClass.name);
    }

}
