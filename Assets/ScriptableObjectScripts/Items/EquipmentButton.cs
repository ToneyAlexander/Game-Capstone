using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CCC.Items;


public class EquipmentButton : MonoBehaviour
{
    private Item _item;
    public Item item
    {
        get { return _item; }
        set { _item = value; }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      
    }
    void OnPointerClick()
    {
        EquipmentSlot[] slots = { EquipmentSlot.Head, EquipmentSlot.Body, EquipmentSlot.Head, EquipmentSlot.Body, EquipmentSlot.Weapon, EquipmentSlot.Offhand, EquipmentSlot.Ring, EquipmentSlot.Amulet };
        int index = transform.GetSiblingIndex();
        InventoryUI selected = transform.parent.parent.gameObject.GetComponent<InventoryUI>();
        selected.euser.Equipment[slots[index]] = Item.Null;
        Debug.Log("WeRolling");
    }
    //void OnMouseOver()
   // {

   // }
}
