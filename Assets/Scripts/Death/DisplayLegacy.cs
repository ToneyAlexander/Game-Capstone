using CCC.Items;
using CCC.Stats;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayLegacy : MonoBehaviour
{
    [SerializeField]
    private Inventory inventory;

    [SerializeField]
    private EquipmentDictionary playerEquipment;

    private Text Display;

    public bool died;

    // Start is called before the first frame update
    void Start()
    {
        Display = GetComponent<Text>();
        Display.text = "Legacy\n";

        //TODO: clear player equipment! or debuff it?
        if (died)
        {
            bool none = true;

            Display.text = Display.text + "Items (50% Less Effective):\n";
            foreach (Item i in inventory.Items)
            {
                none = false;
                WeakenItemDeath(i, .5f, Display);
            }
            if (none)
            {
                Display.text = Display.text + "No Items in Inventory\n";
            }
            else
            {
                none = true;
            }

            Display.text = Display.text + "\n";
            Display.text = Display.text + "Equipment (40% Less Effective):\n";
            foreach (Item i in playerEquipment.Equipment.Values)
            {
                if (!i.Name.Equals(" "))
                {
                    none = false;
                    WeakenItemDeath(i, 4f, Display);
                }
            }
            if (none)
            {
                Display.text = Display.text + "No Items Equipped\n";
            }
            //this.GetComponent<Text>().text += "No buffs to pass down\n";
        }
        else
        {
            bool none = true;

            Display.text = Display.text + "Items (85% Less Effective):\n";
            foreach (Item i in inventory.Items)
            {
                //"heirloom"; "secondhand"; "recycled";
                none = false;
                WeakenItemRetire(i, .85f, Display);
            }
            if (none)
            {
                Display.text = Display.text + "No Items in Inventory\n";
            }
            else
            {
                none = true;
            }

            Display.text = Display.text + "\n";
            Display.text = Display.text + "Equipment (85% Less Effective):\n";
            foreach (Item i in playerEquipment.Equipment.Values)
            {
                if (!i.Name.Equals(" "))
                {
                    none = false;
                    WeakenItemRetire(i, .85f, Display);
                }
            }
            if (none)
            {
                Display.text = Display.text + "No Items Equipped\n";
            }
            //TODO: Passing on buffs
            //this.GetComponent<Text>().text += "Inherited Buff: ";
            //this.GetComponent<Text>().text += "PRINT BUFF\n";
        }
    }

    private void WeakenItemDeath(Item i, float Rate, Text DisplayList)
    {
        i.LongName = i.LongName.Replace("☥ ", "☠️ ");
        i.Name = i.Name.Replace("☥ ", "☠️ ");
        if (!i.LongName.StartsWith("☠️ "))
        {
            i.LongName = "☠️ " + i.LongName;
            i.Name = "☠️ " + i.Name;
        }

        //for each non negative multiply by .5
        DisplayList.text += i.LongName + "\n";
        foreach (Stat s in i.Stats)
        {
            if (s.Value > 0)
            {
                s.Value *= Rate;
            }
        }
    }


    private void WeakenItemRetire(Item i, float Rate, Text DisplayList)
    {
        i.LongName = i.LongName.Replace("☠️ ", "☥ ");
        i.Name = i.Name.Replace("☠️ ", "☥ ");
        if (!i.LongName.StartsWith("☥ "))
        {
            i.LongName = "☥ " + i.LongName;
            i.Name = "☥ " + i.Name;
        }

        //for each non negative multiply by .5
        DisplayList.text += i.LongName + "\n";
        foreach (Stat s in i.Stats)
        {
            if (s.Value > 0)
            {
                s.Value *= Rate;
            }
        }
    }
}
