using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using CCC.Items;
using CCC.Stats;

public class InventoryUI : MonoBehaviour
{
    [SerializeField]
    public Inventory user;

    [SerializeField]
    public EquipmentDictionary euser;

    public int strLength = 30;

    GameObject stored;
    Button[] storedButtons;
    GameObject equipment;
    Button[] equipmentButtons;

    [SerializeField]
    private Color baseColor;

    Image image;
    Text descriptionText;
    Text longNameText;
    GameObject statsBlock;
    Vector3 visibleLoc = new Vector3(-1133, 0, 0);
    Vector3 hiddenLoc = new Vector3(-783, 0, 0);
    bool statsShown = false;

    private void Start()
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
            else if (transform.GetChild(i).name.Equals("StatsBlock"))
            {
                statsBlock = gameObject.transform.GetChild(i).gameObject;
                for (int j = 0; j < statsBlock.transform.childCount; j++)
                {
                    if (statsBlock.transform.GetChild(j).name.Equals("DescText")){
                        descriptionText = statsBlock.transform.GetChild(j).gameObject.GetComponent<Text>();
                    }
                    else if (statsBlock.transform.GetChild(j).name.Equals("SpriteImage")){
                        image = statsBlock.transform.GetChild(j).gameObject.GetComponent<Image>();
                    }else if(statsBlock.transform.GetChild(j).name.Equals("LongName"))
                    {
                        longNameText = statsBlock.transform.GetChild(j).gameObject.GetComponent<Text>();
                    }
                }
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

            EventTrigger.Entry entry3 = new EventTrigger.Entry();
            entry3.eventID = EventTriggerType.PointerExit;
            entry3.callback.AddListener((eventData) => { OnMouseExitEquipment((PointerEventData)eventData); });
            ev.triggers.Add(entry3);

            EquipmentButton equipmentButtonScript = equipmentButtons[i].GetComponent<EquipmentButton>();
            equipmentButtonScript.position = i;
        }
        for (int i = 0; i < storedButtons.Length; i++)
        {
            EventTrigger ev = storedButtons[i].GetComponent<EventTrigger>();
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerClick;
            entry.callback.AddListener((eventData) => { OnClickInventory((PointerEventData)eventData); });
            ev.triggers.Add(entry);

            EventTrigger.Entry entry2 = new EventTrigger.Entry();
            entry2.eventID = EventTriggerType.PointerEnter;
            entry2.callback.AddListener((eventData) => { OnMouseOverInventory((PointerEventData)eventData); });
            ev.triggers.Add(entry2);
            
            InventoryButton inventoryButtonScript = storedButtons[i].GetComponent<InventoryButton>();
            inventoryButtonScript.position = i;

            EventTrigger.Entry entry3 = new EventTrigger.Entry();
            entry3.eventID = EventTriggerType.PointerExit;
            entry3.callback.AddListener((eventData) => { OnMouseExitInventory((PointerEventData)eventData); });
            ev.triggers.Add(entry3);
        }


     //   image = gameObject.transform.GetChild(4).gameObject.GetComponent<Image>();


    }

    private void Update()
    {
        for (int i = 0; i < storedButtons.Length; i++)
        {
            InventoryButton inventoryButtonScript = storedButtons[i].GetComponent<InventoryButton>();
            Image buttonImage = storedButtons[i].transform.GetChild(1).GetComponent<Image>();

            if (user.Items.Count > i)
            {
                Item go = user.Items[i];
                string name = go.Name;
                if (name.Length > strLength)
                {
                    name = name.Substring(0, strLength) + "...";
                }
                inventoryButtonScript.item = go;
                //Debug.Log("[InventoryUI.Update] user = " + user);
                //Debug.Log("[InventoryUI.Update] user.LoadedAssetBundle = " + user.LoadedAssetBundle);
                //Debug.Log("[InventoryUI.Update] inventoryButtonScript = " + inventoryButtonScript);
                //Debug.Log("[InventoryUI.Update] inventoryButtonScript.item = " + inventoryButtonScript.item);
                var sprite = user.LoadedAssetBundle.LoadAsset<Sprite>(inventoryButtonScript.item.SpriteName);
                buttonImage.sprite = sprite;
                buttonImage.color = new Color(1, 1, 1, 1);


            }
            else
            {
                Text textfield = storedButtons[i].GetComponentInChildren<Text>();
                textfield.text = "";
                textfield.color = Color.black;
                inventoryButtonScript.item = Item.Null;
            }
        }
        for (int i = 0; i < 6; i++)
        {
            EquipmentSlot[] slots = { EquipmentSlot.Ring, EquipmentSlot.Amulet, EquipmentSlot.Offhand, EquipmentSlot.Weapon, EquipmentSlot.Body, EquipmentSlot.Head };
            Item go = euser.Equipment[slots[i]];
            EquipmentButton equipmentButtonScript = equipmentButtons[i].GetComponent<EquipmentButton>();
            Image equipmentButtonImage = equipmentButtons[i].transform.GetChild(0).GetComponent<Image>();

            if(go.EquipmentSlot != EquipmentSlot.Null)
            {
                equipmentButtonScript.item = go;
                string name = go.Name;
                if (name.Length > strLength)
                {
                    name = name.Substring(0, strLength) + "...";
                }
                var sprite = user.LoadedAssetBundle.LoadAsset<Sprite>(equipmentButtonScript.item.SpriteName);
                equipmentButtonImage.sprite = sprite;
                equipmentButtonImage.color = new Color(1, 1, 1, 1);
            }
            else
            {
                equipmentButtonImage.sprite = null;
                equipmentButtonImage.color = baseColor;
            }

        }
        if (statsShown)
        {
            statsBlock.transform.localPosition = Vector3.MoveTowards(statsBlock.transform.localPosition, visibleLoc, 900 * Time.deltaTime);

        }
        else
        {
            statsBlock.transform.localPosition = Vector3.MoveTowards(statsBlock.transform.localPosition, hiddenLoc, 900 * Time.deltaTime);
        }

    }

    void OnClickEquipment(BaseEventData data)
    {
		
        EquipmentSlot[] slots = { EquipmentSlot.Ring, EquipmentSlot.Amulet, EquipmentSlot.Offhand, EquipmentSlot.Weapon, EquipmentSlot.Body, EquipmentSlot.Head };
		EquipmentButton equipBut = data.selectedObject.GetComponent<EquipmentButton>();
		if (equipBut != null && equipBut.item != null)
		{
			Debug.Log(equipBut.item.Name);
			euser.DisequipItem(equipBut.item);
		}
        
    } 
    void OnMouseOverEquipment(PointerEventData data)
    {
		//Debug.Log(data.pointerCurrentRaycast.gameObject.transform.parent.GetComponent<EquipmentButton>().item.FlavorText);
		EquipmentButton equipBut = data.pointerCurrentRaycast.gameObject.transform.parent.GetComponent<EquipmentButton>();
		if (equipBut != null && equipBut.item != null)
		{
			descriptionText.text = equipBut.item.FlavorText;
			longNameText.text = data.pointerCurrentRaycast.gameObject.transform.parent.GetComponent<EquipmentButton>().item.LongName;
            //image.sprite = data.pointerCurrentRaycast.gameObject.transform.parent.GetComponent<EquipmentButton>().item.Sprite;
            var sprite = user.LoadedAssetBundle.LoadAsset<Sprite>(data.pointerCurrentRaycast.gameObject.transform.parent.GetComponent<EquipmentButton>().item.SpriteName);
            image.sprite = sprite;
            image.color = new Color(1, 1, 1, 1);
			statsShown = true;
			Item item = data.pointerCurrentRaycast.gameObject.transform.parent.GetComponent<EquipmentButton>().item;
			Text tooltext = statsBlock.GetComponentInChildren<Text>();
			tooltext.text = "";
			foreach (Stat stat in item.Stats)
			{
				int value = (int)stat.Value;
				tooltext.text += stat.Name + ": " + value + "\n";

			}
		}
    }
    void OnClickInventory(PointerEventData data)
    {
        //.GetComponent<Image>();

        if (data.button == PointerEventData.InputButton.Left)
        {
            euser.EquipItem(data.selectedObject.GetComponent<InventoryButton>().item);
        }
        else if (data.button == PointerEventData.InputButton.Right)
        {
            Debug.Log("AAAAAAA");
            
            Debug.Log("Spicy: " + user.Items[0].Name);
            Debug.Log("Butts: " + data.selectedObject.GetComponent<InventoryButton>().item);
            data.pointerCurrentRaycast.gameObject.transform.GetComponent<Image>().sprite = null;
            data.pointerCurrentRaycast.gameObject.transform.GetComponent<Image>().color = new Color(0.13333333333333333333333333333f,0.12549019607f, 0.12549019607f, 1.0f);
            user.RemoveItem(data.selectedObject.GetComponent<InventoryButton>().item);
            euser.CheckAndDisequipItem(data.selectedObject.GetComponent<InventoryButton>().item);

        }
        Debug.Log(user.CurrentCapacity);

    }
    void OnMouseOverInventory(PointerEventData data)
    {
        //Debug.Log(data.pointerCurrentRaycast.gameObject.transform.parent.GetComponent<InventoryButton>().item.FlavorText);
        InventoryButton but = data.pointerCurrentRaycast.gameObject.transform.parent.GetComponent<InventoryButton>();
        if (but.item.EquipmentSlot != EquipmentSlot.Null)
        {

            descriptionText.text = but.item.FlavorText;
            longNameText.text = data.pointerCurrentRaycast.gameObject.transform.parent.GetComponent<InventoryButton>().item.LongName;
            //image.sprite = data.pointerCurrentRaycast.gameObject.transform.parent.GetComponent<InventoryButton>().item.Sprite;
            var sprite = user.LoadedAssetBundle.LoadAsset<Sprite>(data.pointerCurrentRaycast.gameObject.transform.parent.GetComponent<InventoryButton>().item.SpriteName);
            image.sprite = sprite;
            image.color = new Color(1, 1, 1, 1);
            statsShown = true;
            Item item = data.pointerCurrentRaycast.gameObject.transform.parent.GetComponent<InventoryButton>().item;
            Text tooltext = statsBlock.GetComponentInChildren<Text>();
            tooltext.text = "";
            foreach (Stat stat in item.Stats)
            {
                int value = (int)stat.Value;
                tooltext.text += stat.Name + ": " + value + "\n";

            }
        }
    }
    void OnMouseExitEquipment(PointerEventData data)
    {
        statsShown = false;
        image.color = baseColor;

    }
    void OnMouseExitInventory(PointerEventData data)
    {
        statsShown = false;
        image.color = baseColor;
    }

}
