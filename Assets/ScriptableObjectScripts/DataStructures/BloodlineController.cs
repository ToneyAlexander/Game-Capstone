﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "DataStructures/BloodlineController")]
public sealed class BloodlineController : ScriptableObject
{
    [SerializeField]
    private int generationint = 0;
    [SerializeField]
    private int age = 0;

    [SerializeField]
    private string name = "Remy";

    //[SerializeField]
    public ClassPrototype currentClass;

    [SerializeField]
    private List<ClassPrototype> classList = new List<ClassPrototype>();

   // private List<ClassPrototype> pastGenerations = new List<ClassPrototype>();

    
    private List<string> pastNames = new List<string>();

    public void addGeneration()
    {
        pastNames.Add(name + " the " + currentClass.name);
    }
    public void endGeneration(bool retired)
    {
        if (retired)
        {
            pastNames[pastNames.Count - 1] = name + " the " + currentClass.name + "\n" + "Retired at" + age;
        }
        else
        {
            pastNames[pastNames.Count - 1] = name + " the " + currentClass.name + "\n" + "Died at" + age;
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

}