using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using CCC.Items;

public class WorldItemScript : MonoBehaviour
{
    public Item item;

    [SerializeField]
    private Inventory inventory;

    public void setItem(Item i)
    {
        this.item = i;
        //Do other stuff here?
    }

    private void OnMouseDown()
    {
        if (inventory.Items.Count <= inventory.MaxCapacity)
        {
            inventory.Items.Add(item);
            this.gameObject.SetActive(false);
            Destroy(this.gameObject);
        }
    }
}

