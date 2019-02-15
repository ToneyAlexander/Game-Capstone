using CCC.Abilities;
using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Represents a Component that allows a GameObject to use an Ability.
/// </summary>
public sealed class AbilityUser : MonoBehaviour
{
    /// <summary>
    /// The Abilities that this GameObject can use.
    /// </summary>
    [SerializeField]
    private List<Ability> usableAbilities;

    /// <summary>
    /// Use an Ability.
    /// </summary>
    /// <param name="ability">The Ability to use.</param>
    public void Use(Ability ability)
    {
        Debug.Log(gameObject.name + " used Ability " + ability.AbilityName);
    }

    private void Start()
    {
        // Only for testing
        foreach (Ability ability in usableAbilities)
        {
            Use(ability);
        }
    }
}
