using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CCC.Abilities;

public class AbilityHolder : MonoBehaviour
{
    public Ability ability;
    public AbilityPrototype defaultAbility;
    void Start()
    {
        ability = new Ability(defaultAbility);
    }
  
}
