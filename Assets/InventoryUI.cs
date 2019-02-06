using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CCC.Items;

public class InventoryUI : MonoBehaviour
{
    public InventoryUser user;
    public EquipmentUser euser;
    GameObject stored;
    Button[] storedButtons;
    GameObject equipment;
    Button[] equipmentButtons;

    Image image;
    Text descriptionText;
    // Start is called before the first frame update
    void Start()
    {
        stored = gameObject.transform.GetChild(1).gameObject;
        storedButtons = stored.GetComponentsInChildren<Button>();
        equipment = gameObject.transform.GetChild(2).gameObject;
        equipmentButtons = stored.GetComponentsInChildren<Button>();
        image = gameObject.transform.GetChild(2).gameObject.GetComponent<Image>();


    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < storedButtons.Length; i++)
        {
            Item go = user.Inventory.Items[i];
            Text textfield = storedButtons[i].GetComponent<Text>();
            textfield.text = go.Name;
            textfield.color = Color.black;
        }
        for (int i = 0; i < equipmentButtons.Length; i++)
        {
            EquipmentSlot[] slots = { EquipmentSlot.Head, EquipmentSlot.Body, EquipmentSlot.Head, EquipmentSlot.Body, EquipmentSlot.Weapon, EquipmentSlot.Offhand, EquipmentSlot.Ring, EquipmentSlot.Amulet};
            Item go = euser.Equipment.Equipment[slots[i]];
            Text textfield = equipmentButtons[i].GetComponent<Text>();
            textfield.text = go.Name;
            textfield.color = Color.black;
        }

    }
}
