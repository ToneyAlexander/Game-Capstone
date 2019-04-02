using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CCC.Items;


public class InventoryButton : MonoBehaviour
{
    private Item _item;
    private int _position;
    public Item item
    {
        get { return _item; }
        set { _item = value; }
    }
    public int position
    {
        get { return _position; }
        set { _position = value; }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void onClick()
    {
        int index = transform.GetSiblingIndex();
        InventoryUI selected = transform.parent.parent.gameObject.GetComponent<InventoryUI>();
        selected.euser.EquipItem(selected.user.Items[index]);
    }
}
