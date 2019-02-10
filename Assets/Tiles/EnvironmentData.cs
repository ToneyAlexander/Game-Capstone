﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EnvironmentData : ScriptableObject
{
    [SerializeField]
    public List<GameObject> EnvironmentList;

    // Start is called before the first frame update
    public EnvironmentData()
    {
        EnvironmentList = new List<GameObject>();
    }
}
