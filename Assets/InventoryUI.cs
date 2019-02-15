using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
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
        for (int i = 0; i < 6; i++)
        {
            EventTrigger ev = equipmentButtons[i].GetComponent<EventTrigger>();
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerClick;
            entry.callback.AddListener((eventData) => { OnClickEquipment(eventData); });
            ev.triggers.Add(entry);
            EventTrigger.Entry entry2 = new EventTrigger.Entry();
            entry2.eventID = EventTriggerType.PointerEnter;
            entry2.callback.AddListener((eventData) => { OnMouseOverEquipment((PointerEventData)eventData); });
            ev.triggers.Add(entry2);
            EquipmentButton equipmentButtonScript = equipmentButtons[i].GetComponent<EquipmentButton>();
            equipmentButtonScript.position = i;
        }
        for (int i = 0; i < storedButtons.Length; i++)
        {
            EventTrigger ev = storedButtons[i].GetComponent<EventTrigger>();
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerClick;
            entry.callback.AddListener((eventData) => { OnClickInventory(eventData); });
            ev.triggers.Add(entry);
            EventTrigger.Entry entry2 = new EventTrigger.Entry();
            entry2.eventID = EventTriggerType.PointerEnter;
            entry2.callback.AddListener((eventData) => { OnMouseOverInventory((PointerEventData)eventData); });
            ev.triggers.Add(entry2);
            InventoryButton inventoryButtonScript = storedButtons[i].GetComponent<InventoryButton>();
            inventoryButtonScript.position = i;
        }

     //   image = gameObject.transform.GetChild(4).gameObject.GetComponent<Image>();


    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < storedButtons.Length; i++)
        {
            InventoryButton inventoryButtonScript = storedButtons[i].GetComponent<InventoryButton>();

            if (user.Items.Count > i)
            {
                Item go = user.Items[i];
                Text textfield = storedButtons[i].GetComponentInChildren<Text>();
                textfield.text = go.Name;
                textfield.color = Color.black;
                inventoryButtonScript.item = go;


            }
            else
            {
                Text textfield = storedButtons[i].GetComponentInChildren<Text>();
                textfield.text = "--";
                textfield.color = Color.black;
                inventoryButtonScript.item = Item.Null;
            }
        }
        for (int i = 0; i < 6; i++)
        {
                EquipmentSlot[] slots = { EquipmentSlot.Ring, EquipmentSlot.Amulet, EquipmentSlot.Offhand, EquipmentSlot.Weapon, EquipmentSlot.Body, EquipmentSlot.Head };
                Item go = euser.Equipment[slots[i]];
                Text textfield = equipmentButtons[i].GetComponentInChildren<Text>();
                EquipmentButton equipmentButtonScript = equipmentButtons[i].GetComponent<EquipmentButton>();
                equipmentButtonScript.item = go;
                textfield.text = go.Name;
                textfield.color = Color.black;
                
        }

    }

    void OnClickEquipment(BaseEventData data)
    {
        EquipmentSlot[] slots = { EquipmentSlot.Ring, EquipmentSlot.Amulet, EquipmentSlot.Offhand, EquipmentSlot.Weapon, EquipmentSlot.Body, EquipmentSlot.Head };
        Debug.Log(data.selectedObject.GetComponent<EquipmentButton>().item.Name);
        euser.DisequipItem(data.selectedObject.GetComponent<EquipmentButton>().item);
        
    } 
    void OnMouseOverEquipment(PointerEventData data)
    {
       Debug.Log(data.pointerCurrentRaycast.gameObject.transform.parent.GetComponent<EquipmentButton>().item.FlavorText);
       descriptionText.text = data.pointerCurrentRaycast.gameObject.transform.parent.GetComponent<EquipmentButton>().item.FlavorText;
       image.sprite = data.pointerCurrentRaycast.gameObject.transform.parent.GetComponent<EquipmentButton>().item.Sprite;
    }
    void OnClickInventory(BaseEventData data)
    {
        euser.EquipItem(data.selectedObject.GetComponent<InventoryButton>().item);
    }
    void OnMouseOverInventory(PointerEventData data)
    {
        Debug.Log(data.pointerCurrentRaycast.gameObject.transform.parent.GetComponent<InventoryButton>().item.FlavorText);
        descriptionText.text = data.pointerCurrentRaycast.gameObject.transform.parent.GetComponent<InventoryButton>().item.FlavorText;
        image.sprite = data.pointerCurrentRaycast.gameObject.transform.parent.GetComponent<InventoryButton>().item.Sprite;

    }

}
