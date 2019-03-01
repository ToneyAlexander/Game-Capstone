using CCC.Abilities;
using CCC.Stats;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilitySlotController : MonoBehaviour
{

    private Image coolDownImage;

    [SerializeField]
    private Image abilityImage;

    [SerializeField]
    private AbilitySlotDictionary abilitiyDictionary;

    private bool isInCoolDown;

	private float cdMax;

	private GameObject SlotOne;

	AbilitySlot[] abilityArray;

	Dictionary<int, Ability> abilities;

	Transform Slot1, Slot2;

	Dictionary<Transform, Ability> Slots;

	private void Awake()
	{
		Slots = new Dictionary<Transform, Ability>();
	}

    private void Start()
    {
        isInCoolDown = false;
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
				cd_image.fillAmount = 1 - (max - remain);
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
		int slotAmount = 1;
	
		for(int i = 0; i <= slotAmount; i++)
		{
			Transform slot = this.transform.GetChild(i);
			Ability ability = abilitiyDictionary.GetAbility(abilityArray[i]);
			if (ability != null)
			{
				Slots.Add(slot, ability);
				SetIcon(slot, i);
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
				child.GetComponent<Image>().sprite = Slots[slot].Icon;
			}
		}
	}
}
