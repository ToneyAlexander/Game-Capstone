using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CCC.Items;


public class EquipmentButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      
    }
    void OnClick()
    {
        EquipmentSlot[] slots = { EquipmentSlot.Head, EquipmentSlot.Body, EquipmentSlot.Head, EquipmentSlot.Body, EquipmentSlot.Weapon, EquipmentSlot.Offhand, EquipmentSlot.Ring, EquipmentSlot.Amulet };
        int index = transform.GetSiblingIndex();
        InventoryUI selected = transform.parent.parent.gameObject.GetComponent<InventoryUI>();
        selected.euser.Equipment.Equipment[slots[index]] = Item.Null;
    }
    void OnMouseOver()
    {

    }
}
