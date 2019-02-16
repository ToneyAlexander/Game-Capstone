using System.Collections.Generic;
using CCC.Abilities;
using UnityEngine;

/// <summary>
/// Represents a Component that causes a GameObject to detect input for certain 
/// buttons represented by a list of button names and report it to a 
/// CommandProcessor.
/// </summary>
[RequireComponent(typeof(AbilityUser))]
[RequireComponent(typeof(MousePositionDetector))]
public sealed class AbilityInputDetector : MonoBehaviour
{
    /// <summary>
    /// The AbilitySlotDictionary that defines each Ability in each AbilitySlot.
    /// </summary>
    [SerializeField]
    private AbilitySlotDictionary abilitySlotMappings;

    #region Components
    /// <summary>
    /// The AbilityUser Component of the GameObject that this 
    /// AbilityInputDetector Component is attached to.
    /// </summary>
    private AbilityUser abilityUser;

    /// <summary>
    /// The MousePositionDetector Component of the GameObject that this 
    /// MousePositionDetector Component is attached to.
    /// </summary>
    private MousePositionDetector mousePositionDetector;
    #endregion

    /// <summary>
    /// The CommandProcessor that this AbilityInputDetector sends ICommands to.
    /// </summary>
    [SerializeField]
    private CommandProcessor commandProcessor;

    /// <summary>
    /// The dictionary of InputButton to AbilitySlot that determines which 
    /// InputButtons this AbilityInputDetector listens for and which 
    /// AbilitySlots each of those InputButtons corresponds to.
    /// </summary>
    private Dictionary<InputButton, AbilitySlot> buttonMappings;

    #region InputButtons
    /// <summary>
    /// The InputButton for AbilitySlot.One.
    /// </summary>
    [SerializeField]
    private InputButton abilitySlotOneButton;

    /// <summary>
    /// The InputButton for AbilitySlot.Two.
    /// </summary>
    [SerializeField]
    private InputButton abilitySlotTwoButton;

    /// <summary>
    /// The InputButton for AbilitySlot.Three.
    /// </summary>
    [SerializeField]
    private InputButton abilitySlotThreeButton;

    /// <summary>
    /// The InputButton for AbilitySlot.Four.
    /// </summary>
    [SerializeField]
    private InputButton abilitySlotFourButton;

    /// <summary>
    /// The InputButton for AbilitySlot.Five.
    /// </summary>
    [SerializeField]
    private InputButton abilitySlotFiveButton;
    #endregion

    #region MonoBehaviour Messages
    private void Awake()
    {
        abilityUser = GetComponent<AbilityUser>();
        mousePositionDetector = GetComponent<MousePositionDetector>();
    }

    private void Start()
    {
        buttonMappings = new Dictionary<InputButton, AbilitySlot> {
            {abilitySlotOneButton, AbilitySlot.One},
            {abilitySlotTwoButton, AbilitySlot.Two},
            {abilitySlotThreeButton, AbilitySlot.Three},
            {abilitySlotFourButton, AbilitySlot.Four},
            {abilitySlotFiveButton, AbilitySlot.Five}
        };
    }

    private void Update()
    {
        foreach (InputButton button in buttonMappings.Keys)
        { 
            if (Input.GetButtonDown(button.Name))
            {
                AbilitySlot slot = buttonMappings[button];
                ICommand command = new UseAbilityCommand(
                    abilityUser,
                    abilitySlotMappings.GetAbility(slot),
                    mousePositionDetector.CalculateWorldPosition()
                );
                commandProcessor.ProcessCommand(command);
            }
        }
    }
    #endregion
}
