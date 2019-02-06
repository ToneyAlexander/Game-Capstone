using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CCC.Items;

public class InventoryUI : MonoBehaviour
{
    [SerializeField]
    public Inventory user;

    [SerializeField]
    public EquipmentDictionary euser;

    GameObject stored;
    Button[] storedButtons;
    GameObject equipment;
    Button[] equipmentButtons;

    Image image;
    Text descriptionText;
    // Start is called before the first frame update
    void Start()
    {
        stored = gameObject.transform.GetChild(2).gameObject;
        storedButtons = stored.GetComponentsInChildren<Button>();
        equipment = gameObject.transform.GetChild(3).gameObject;
        equipmentButtons = stored.GetComponentsInChildren<Button>();
     //   image = gameObject.transform.GetChild(4).gameObject.GetComponent<Image>();


    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < storedButtons.Length; i++)
        {
            if (user.Items.Count > 0)
            {
                Item go = user.Items[i];
                Text textfield = storedButtons[i].GetComponent<Text>();
                textfield.text = go.Name;
                textfield.color = Color.black;
            }
        }
        for (int i = 0; i < 6; i++)
        {
                EquipmentSlot[] slots = { EquipmentSlot.Head, EquipmentSlot.Body, EquipmentSlot.Head, EquipmentSlot.Body, EquipmentSlot.Weapon, EquipmentSlot.Offhand, EquipmentSlot.Ring, EquipmentSlot.Amulet };
                Item go = euser.Equipment[slots[i]];
                Text textfield = equipmentButtons[i].GetComponentInChildren<Text>();
                textfield.text = go.Name;
                textfield.color = Color.black;
        }

    }
}
