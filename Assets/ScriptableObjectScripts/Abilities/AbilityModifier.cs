using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AbilityModifier
{
    public string AbilityName
    {
        get { return abilityName; }
    }

    public AbilityStatIdentifier StatName
    {
        get { return statName; }
    }

    public float Value;

    [SerializeField]
    private string abilityName;

    [SerializeField]
    private AbilityStatIdentifier statName;
}
