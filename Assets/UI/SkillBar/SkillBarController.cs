using CCC.Abilities;
using UnityEngine;
using System.Collections.Generic;

public sealed class SkillBarController : MonoBehaviour
{
    [SerializeField]
    private AbilitySlotDictionary abilityDictionary = null;

    [SerializeField]
    private List<SkillBarSlotController> slotControllers = null;

    #region MonoBehaviour Messages
    private void Update()
    {
        Debug.Log(slotControllers.Count);
        var abilities = abilityDictionary.Abilities;
        for (var i = 0; i < slotControllers.Count; i++)
        {
            if (abilities[i].AbilityName != Ability.Null.AbilityName)
            {
                Debug.Log(abilities[i].AbilityName);
                slotControllers[i].BindAbility(abilities[i]);
                //Debug.Log("Binded ability for slotController " + slotControllers[i].name);
            }
        }
    }
    #endregion
}