using CCC.Abilities;
using UnityEngine;

/// <summary>
/// Represents an ICommand that causes a GameObject with an AbilityUser 
/// Component to cast a given Ability.
/// </summary>
public sealed class UseAbilityCommand : ICommand
{
    /// <summary>
    /// The Ability that this UseAbilitCommand's AbilityUser will use.
    /// </summary>
    private readonly Ability ability;

    /// <summary>
    /// The AbilityUser that will use this UseAbilityCommand's Ability.
    /// </summary>
    private readonly AbilityUser abilityUser;

    /// <summary>
    /// The position of the mouse in world space during the frame that this 
    /// UseAbilityCommand was created.
    /// </summary>
    private readonly Vector3 mouseWorldPosition;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:UseAbilityCommand"/> class.
    /// </summary>
    /// <param name="abilityUser">
    /// A GameObject's AbilityUser Component that will use the given Ability.
    /// </param>
    /// <param name="ability">
    /// The Ability that the given AbilityUser Component will use.
    /// </param>
    /// <param name="mouseWorldPosition">
    /// The position of the mouse in world space during the current frame.
    /// </param>
    public UseAbilityCommand(AbilityUser abilityUser, Ability ability, 
        Vector3 mouseWorldPosition)
    {
        this.ability = ability;
        this.abilityUser = abilityUser;
        this.mouseWorldPosition = mouseWorldPosition;
    }

    #region ICommand Methods
    public void InvokeCommand()
    {
        abilityUser.Use(ability, mouseWorldPosition);
    }
    #endregion
}
