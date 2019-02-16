using System.Collections.Generic;
using CCC.Abilities;
using UnityEngine;

/// <summary>
/// Represents a Component that causes a GameObject to detect input for certain 
/// buttons represented by a list of button names and report it to a 
/// CommandProcessor.
/// </summary>
[RequireComponent(typeof(AbilityUser))]
public sealed class AbilityInputDetector : MonoBehaviour
{
    [SerializeField]
    private CommandProcessor commandProcessor;

    [SerializeField]
    private InputButton abilitySlotOneButton;

    [SerializeField]
    private InputButton abilitySlotTwoButton;

    [SerializeField]
    private InputButton abilitySlotThreeButton;

    [SerializeField]
    private InputButton abilitySlotFourButton;

    [SerializeField]
    private InputButton abilitySlotFiveButton;

    private AbilityUser abilityUser;

    private Dictionary<InputButton, AbilitySlot> buttonMappings;

    [SerializeField]
    private AbilitySlotDictionary abilitySlotMappings;

    private void Awake()
    {
        abilityUser = GetComponent<AbilityUser>();
    }

    private void OnEnable()
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
                    abilitySlotMappings.GetAbility(slot)
                );
                commandProcessor.ProcessCommand(command);
            }
        }
    }
}
