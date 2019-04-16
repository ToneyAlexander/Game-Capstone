using CCC.Stats;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using cakeslice;

public class ItemHover : MonoBehaviour
{
    private WorldItemScript WIScript;

    private GameObject ItemDisplay;

    private Text itemName;

    private Text description;

    private Text stats;

    private cakeslice.Outline line;

    void Start()
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("HUDElement");
        for (int i = 0; i < gameObjects.Length; i++)
        {
            if (gameObjects[i].name.Equals("HUD"))
            {
                GameObject HUDElement = gameObjects[i];
                for(int j = 0; j < HUDElement.transform.childCount; j++)
                {
                    if (HUDElement.transform.GetChild(j).name.Equals("ItemDesc"))
                    {
                        ItemDisplay = HUDElement.transform.GetChild(j).gameObject;
                        itemName = ItemDisplay.transform.GetChild(1).GetComponent<Text>();
                        description = ItemDisplay.transform.GetChild(2).GetComponent<Text>();
                        stats = ItemDisplay.transform.GetChild(3).GetComponent<Text>();
                    }
                }
            }
        }
        line = GetComponentInChildren<cakeslice.Outline>();
        WIScript = this.transform.GetComponent<WorldItemScript>();
        ItemDisplay.SetActive(false);
    }

    void OnMouseOver()
    {
        ItemDisplay.SetActive(true);
        itemName.text = WIScript.item.LongName;
        description.text = WIScript.item.FlavorText;
        stats.text = "";
        foreach (Stat stat in WIScript.item.Stats)
        {
            string value = Stat.GetStatString(stat);
            stats.text += stat.Name + ": " + value + "\n";
        }
        line.color = 0;
    }

    private void OnMouseExit()
    {
        ItemDisplay.SetActive(false);
        itemName.text = "";
        stats.text = "";
        description.text = "";
        line.color = 1;
    }

    private void OnMouseDown()
    {
        ItemDisplay.SetActive(false);
        itemName.text = "";
        stats.text = "";
        description.text = "";
        line.color = 1;
    }
}
