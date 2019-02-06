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
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).name.Equals("Stored"))
            {
                stored = gameObject.transform.GetChild(i).gameObject;
                storedButtons = stored.GetComponentsInChildren<Button>();
            }
            else if (transform.GetChild(i).name.Equals("Equipment"))
            {
                equipment = gameObject.transform.GetChild(i).gameObject;
                equipmentButtons = equipment.GetComponentsInChildren<Button>();
            }
            else if (transform.GetChild(i).name.Equals("SpriteImage"))
            {
                image = gameObject.transform.GetChild(i).gameObject.GetComponent<Image>();
            }
            else if (transform.GetChild(i).name.Equals("DescText"))
            {
                descriptionText = gameObject.transform.GetChild(i).gameObject.GetComponent<Text>();
            }
        }
     //   image = gameObject.transform.GetChild(4).gameObject.GetComponent<Image>();


    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < storedButtons.Length; i++)
        {
            if (user.Items.Count > i)
            {
                Item go = user.Items[i];
                Text textfield = storedButtons[i].GetComponentInChildren<Text>();
                textfield.text = go.Name;
                textfield.color = Color.black;
            }
            else
            {
                Text textfield = storedButtons[i].GetComponentInChildren<Text>();
                textfield.text = "Straight and to the Point";
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
