using CCC.Abilities;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;

public class AbilityUI : MonoBehaviour
{
    GameObject slotsHolder;
    GameObject storedAbils;
    GameObject[] statSlots = new GameObject[6];
    int selected = -1;
    public AbilitySlotDictionary dict;
    public AbilitySet fullSet;

    #region MonoBehaviour Messages
    private void Start()
    {
        for (int i = 0; i < this.transform.childCount; i++)
        {

            if (this.transform.GetChild(i).name.Equals("Slots"))
            {

                slotsHolder = this.transform.GetChild(i).gameObject;

                for (int j = 0; j < slotsHolder.transform.childCount; j++)
                {
                    if (slotsHolder.transform.GetChild(j).name.Equals("Slot1"))
                    {
                        statSlots[0] = slotsHolder.transform.GetChild(j).gameObject;
                    }
                    if (slotsHolder.transform.GetChild(j).name.Equals("Slot2"))
                    {
                        statSlots[1] = slotsHolder.transform.GetChild(j).gameObject;
                    }
                    if (slotsHolder.transform.GetChild(j).name.Equals("Slot3"))
                    {
                        statSlots[2] = slotsHolder.transform.GetChild(j).gameObject;
                    }
                    if (slotsHolder.transform.GetChild(j).name.Equals("Slot4"))
                    {
                        statSlots[3] = slotsHolder.transform.GetChild(j).gameObject;
                    }
                    if (slotsHolder.transform.GetChild(j).name.Equals("Slot5"))
                    {
                        statSlots[4] = slotsHolder.transform.GetChild(j).gameObject;
                    }
                    if (slotsHolder.transform.GetChild(j).name.Equals("Slot6"))
                    {
                        statSlots[5] = slotsHolder.transform.GetChild(j).gameObject;
                    }


                }

            }
            if (this.transform.GetChild(i).name.Equals("Abilities"))
            {
                storedAbils = this.transform.GetChild(i).gameObject;

            }

        }
        for (int i = 0; i < statSlots.Length; i++)
        {
            EventTrigger ev = statSlots[i].GetComponent<EventTrigger>();
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerClick;
            entry.callback.AddListener((eventData) => { SlotOnClick((PointerEventData)eventData); });
            ev.triggers.Add(entry);
        }
        for (int i = 0; i < storedAbils.GetComponentsInChildren<EventTrigger>().Length; i++)
        {
            EventTrigger ev = storedAbils.transform.GetChild(i).GetComponent<EventTrigger>();
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerClick;
            entry.callback.AddListener((eventData) => { AbilityOnClick((PointerEventData)eventData); });
            ev.triggers.Add(entry);
        }
    }

    private void Update()
    {
        for (int i = 0; i < statSlots.Length; i++)
        {
            statSlots[i].GetComponent<Image>().color = Color.white;
            Image img = statSlots[i].GetComponent<Image>();
            if (i == 0)
            {
                setIcon(img, AbilitySlot.One);
            }
            else if (i == 1)
            {
                setIcon(img, AbilitySlot.Two);
            }
            else if (i == 2)
            {
                setIcon(img, AbilitySlot.Three);
            }
            else if (i == 3)
            {
                setIcon(img, AbilitySlot.Four);
            }
            else if (i == 4)
            {
                setIcon(img, AbilitySlot.Five);
            }
            else if (i == 5)
            {
                setIcon(img, AbilitySlot.Six);
            }
        }
        if (selected >= 1)
        {
            statSlots[selected - 1].GetComponent<Image>().color = Color.red;
        }
        int count = 0;
        foreach (KeyValuePair<string, Ability> abil in fullSet.Set)
        {
            GameObject store = storedAbils.transform.GetChild(count).gameObject;
            store.GetComponent<Image>().sprite = dict.AbilityIconsAssetBundle.LoadAsset<Sprite>(abil.Value.SpriteFilename);
            AbilityHolder x = store.GetComponent<AbilityHolder>();
            x.ability = abil.Value;
            count++;
        }
    }
    #endregion MonoBehaviour Messages

    private void setIcon(Image img, AbilitySlot slot)
    {
        Ability abil = dict.GetAbility(slot);
        if (abil != Ability.nullAbility)
        {
            img.sprite = dict.AbilityIconsAssetBundle.LoadAsset<Sprite>(abil.SpriteFilename);
        }
    }
    void AbilityOnClick(PointerEventData data)
    {
        GameObject clicked = data.pointerCurrentRaycast.gameObject;
        Ability ability = clicked.GetComponent<AbilityHolder>().ability;
        if (selected >= 1 && ability.AbilityName != "Null Ability")
        {


            if (selected == 1)
            {
                dict.SetSlotAbility(AbilitySlot.One, ability);
            }
            else if (selected == 2)
            {
                dict.SetSlotAbility(AbilitySlot.Two, ability);
            }
            else if (selected == 3)
            {
                dict.SetSlotAbility(AbilitySlot.Three, ability);
            }
            else if (selected == 4)
            {
                dict.SetSlotAbility(AbilitySlot.Four, ability);
            }
            else if (selected == 5)
            {
                dict.SetSlotAbility(AbilitySlot.Five, ability);
            }
            else if (selected == 6)
            {
                dict.SetSlotAbility(AbilitySlot.Six, ability);
            }

        }
        selected = -1;
    }
    void SlotOnClick(PointerEventData data)
    {
        GameObject clicked = data.pointerCurrentRaycast.gameObject;
        if (clicked.name == "Slot1")
        {
            if (selected == 1)
            {
                selected = -1;
            }
            else
            {
                selected = 1;
            }
        }
        if (clicked.name == "Slot2")
        {
            if (selected == 2)
            {
                selected = -1;
            }
            else
            {
                selected = 2;
            }
        }
        if (clicked.name == "Slot3")
        {
            if (selected == 3)
            {
                selected = -1;
            }
            else
            {
                selected = 3;
            }
        }
        if (clicked.name == "Slot4")
        {
            if (selected == 4)
            {
                selected = -1;
            }
            else
            {
                selected = 4;
            }
        }
        if (clicked.name == "Slot5")
        {
            if (selected == 5)
            {
                selected = -1;
            }
            else
            {
                selected = 5;
            }
        }
        if (clicked.name == "Slot6")
        {
            if (selected == 6)
            {
                selected = -1;
            }
            else
            {
                selected = 6;
            }
        }

    }
}
