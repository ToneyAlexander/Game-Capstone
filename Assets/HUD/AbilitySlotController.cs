using CCC.Abilities;
using CCC.Stats;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilitySlotController : MonoBehaviour
{

    [SerializeField]
    private AbilitySlotDictionary abilitiyDictionary;

	AbilitySlot[] abilityArray;

	Dictionary<int, Ability> abilities;

	Dictionary<Transform, Ability> Slots;

	private void Awake()
	{
		Slots = new Dictionary<Transform, Ability>();
	}

    private void Start()
    {
		abilityArray = (AbilitySlot[])Enum.GetValues(typeof(AbilitySlot));
		SetSlots();
	}

    void Update()
    {
		RefreshHotbar(); //get rid of this shit, someone call the hotbar refresh
        UpdateCoolDown();
    }

	private void UpdateCoolDown()
	{
		foreach(Transform slot in Slots.Keys)
		{
			Ability test = Slots[slot];
			if(Slots[slot] != Ability.nullAbility)
			{
				GameObject icon = slot.GetChild(0).gameObject;
				Image cd_image = icon.transform.GetChild(1).gameObject.GetComponent<Image>();
				Ability slot2 = Slots[slot];
				float remain = slot2.cdRemain;
				float max = slot2.Stats.Find(item => item.Name == Stat.AS_CD).Value;
                cd_image.fillAmount = remain / max; 
			}
		}
	}

	/*
	 * Refreshes HUD when new changes are made to it
	 */
	private void RefreshHUD()
	{

	}

	/*
	 * Refreshes Hotbar specifically
	 */
	private void RefreshHotbar()
	{
		Slots = new Dictionary<Transform, Ability>(); //empty dictionary
		SetSlots();
	}
	/*
	 * Initially sets slots in HUD
	 */ 
	private void SetSlots()
	{
		int slotAmount = 5;
	
		for(int i = 0; i <= slotAmount-1; i++)
		{
			Transform slot = this.transform.GetChild(i);
			Ability ability = abilitiyDictionary.GetAbility(abilityArray[i]);
            if (ability != Ability.nullAbility)
            {
                Slots.Add(slot, ability);
                SetIcon(slot, i);
            }
            else
            {
                Image img = slot.GetChild(i).GetComponent<Image>();
                img.sprite = null;
                img.color = new Color(255, 255, 255, 0);
            }
		}
	}

	
	private void SetIcon(Transform slot, int slotNum)
	{
		for(int i = 0; i < 2; i++)
		{
			Transform child = slot.GetChild(i);
			if(child.name.Equals("Image"))
			{
                Image img = child.GetComponent<Image>();
                img.sprite = Slots[slot].Icon;
                img.color = new Color(255, 255, 255, 255);
            }
		}
	}
}
