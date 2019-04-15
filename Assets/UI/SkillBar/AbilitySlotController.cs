using CCC.Abilities;
using CCC.Stats;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilitySlotController : MonoBehaviour
{

    [SerializeField]
    private AbilitySlotDictionary abilityDictionary = null;

	AbilitySlot[] abilityArray;

	Dictionary<Transform, Ability> Slots;

    [SerializeField]
    private GameObject slotList;

    [SerializeField]
    private List<GameObject> slots;

    private List<Ability> abilities;

	private void Awake()
	{
		//Slots = new Dictionary<Transform, Ability>();
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
		//foreach(Transform slot in Slots.Keys)
		//{
		//	Ability test = Slots[slot];
		//	if(Slots[slot] != Ability.nullAbility)
		//	{
		//		GameObject icon = slot.GetChild(0).gameObject;
		//		Image cd_image = icon.transform.GetChild(0).gameObject.GetComponent<Image>();
		//		Ability slot2 = Slots[slot];
		//		float remain = slot2.cdRemain;
		//		float max = slot2.Stats.Find(item => item.Name == Stat.AS_CD).Value;
  //              cd_image.fillAmount = remain / max; 
		//	}
		//}

        for (var i = 0; i < slots.Count; i++)
        {
            var iconGameObject = slots[i].transform.GetChild(0).gameObject;
            var cdImage = iconGameObject.transform.GetChild(0).gameObject.GetComponent<Image>();
            var ability = abilityDictionary.GetAbility(abilityArray[i]);
            var remain = ability.cdRemain;
            var max = ability.Stats.Find(item => item.Name == Stat.AS_CD).Value;
            cdImage.fillAmount = remain / max;
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

        for (int i = 0; i <= slotAmount; i++)
		{
			Transform slot = this.transform.GetChild(i);
			Ability ability = abilityDictionary.GetAbility(abilityArray[i + 1]);
            if (ability != Ability.Null)
            {
                Slots.Add(slot, ability);
                SetIcon(slot, i);
            }
		}

        for (var i = 0; i < slotAmount; i++)
        {
            var slotTransform = slotList.transform.GetChild(i);
            var ability = abilityDictionary.GetAbility(abilityArray[i + 1]);
        }
    }

	
	private void SetIcon(Transform slot, int slotNum)
	{
		Transform child = slot.GetChild(0);
		Image img = child.GetComponent<Image>();
        if (Slots[slot] != Ability.Null)
        {
            img.sprite = abilityDictionary.AbilityIconsAssetBundle.LoadAsset<Sprite>(Slots[slot].SpriteFilename);
        }
        img.color = new Color(255, 255, 255, 255);
	}
}
