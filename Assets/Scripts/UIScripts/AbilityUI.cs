using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using CCC.Abilities;

public class AbilityUI : MonoBehaviour
{
    GameObject holder;
    GameObject slotsHolder;
    GameObject[] statSlots = new GameObject[5];
    int selected = -1;
    public AbilitySlotDictionary dict;
    // Start is called before the first frame update
    void Start()
    {
        holder = transform.GetChild(0).gameObject;
        for (int i = 0; i < holder.transform.childCount; i++)
        {
            if (holder.transform.GetChild(i).name.Equals("Slots"))
            {
                slotsHolder = transform.GetChild(i).gameObject;
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


                }

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
    }
    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < statSlots.Length; i++)
        {
            statSlots[i].GetComponent<Image>().color = Color.grey;
        }
        if (selected >= 1)
        {
           statSlots[selected - 1].GetComponent<Image>().color = Color.red;
        }
    }
    void AbilityOnClick(PointerEventData data)
    {
        GameObject clicked = data.pointerCurrentRaycast.gameObject;
       if (selected >= 1)
            {
               //dict.SetAbility() 
            }
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

    }
}
