using CCC.Items;
using CCC.Stats;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayLegacy : MonoBehaviour
{
    [SerializeField]
    Inventory inventory;


    // Start is called before the first frame update
    void Start()
    {
        bool died = true;
        this.GetComponent<Text>().text = "Legacy\n";
        if (died)
        {
            this.GetComponent<Text>().text += "Items (50% Less Effective):\n";
            foreach (Item i in inventory.Items)
            {
                //for each non negative multiply by .5
                this.GetComponent<Text>().text += i.LongName + "\n";
                foreach (Stat s in i.Stats)
                {
                    if (s.Value > 0)
                    {
                        s.Value *= .5f;
                    }
                }
            }
            //this.GetComponent<Text>().text += "No buffs to pass down\n";
        }
        else
        {
            this.GetComponent<Text>().text += "Items (85% Less Effective):\n";
            foreach (Item i in inventory.Items)
            {
                //for each non negative multiply by .85
                this.GetComponent<Text>().text += i.LongName + "\n";
                foreach (Stat s in i.Stats)
                {
                    if (s.Value > 0)
                    {
                        s.Value *= .85f;
                    }
                }
            }
            //TODO: Passing on buffs
            //this.GetComponent<Text>().text += "Inherited Buff: ";
            //this.GetComponent<Text>().text += "PRINT BUFF\n";
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
