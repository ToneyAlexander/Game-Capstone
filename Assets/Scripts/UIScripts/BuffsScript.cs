using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BuffsScript : MonoBehaviour
{

	private Dictionary<string, TimedBuff> storedBuffs;

	private GameObject[] storedBuffsObjects; 

	private List<TimedBuff> buffs;
    
    private ControlStatBlock player;

	private GameObject column4;

	private GameObject toolTip;

	private int previousSize;

	private int currentSize;
    
    // Start is called before the first frame update
    void Start()
    {
		for (int i = 0; i < transform.childCount; i++)
		{
			if (transform.GetChild(i).name.Equals("Column4"))
			{
				column4 = transform.GetChild(i).gameObject;
			}
			else if (transform.GetChild(i).name.Equals("ToolTip"))
			{
				toolTip = transform.GetChild(i).gameObject;
			}
		}
		toolTip.SetActive(false);
		storedBuffs = new Dictionary<string, TimedBuff>();
		buffs = new List<TimedBuff>();
		int count = column4.transform.childCount;
		storedBuffsObjects = new GameObject[count];

		GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Player");
        if (gameObjects.Length > 0)
        {
            player = gameObjects[0].GetComponent<ControlStatBlock>();
        }
		
		for(int k = 0; k < column4.transform.childCount; k++)
		{
			storedBuffsObjects[k] = column4.transform.GetChild(k).gameObject;
			EventTrigger ev = storedBuffsObjects[k].GetComponent<EventTrigger>();
			EventTrigger.Entry entry = new EventTrigger.Entry();
			entry.eventID = EventTriggerType.PointerEnter;
			entry.callback.AddListener((eventData) => { OnHoverBuff((PointerEventData)eventData); });
			ev.triggers.Add(entry);

			EventTrigger.Entry leave = new EventTrigger.Entry();
			leave.eventID = EventTriggerType.PointerExit;
			leave.callback.AddListener((eventData) => { OnLeaveBuff((PointerEventData)eventData); });
			ev.triggers.Add(leave);
		}
		previousSize = 0;
		currentSize = 0;
	}

    // Update is called once per frame
    void Update()
    {
		// trimming to reduced list
		buffs = player.buffs;
		currentSize = buffs.Count;
		storedBuffs.Clear();
		for (int i = 0; i < buffs.Count; i++)
		{
			string name = buffs[i].BuffName;
			if (!storedBuffs.ContainsKey(name))
			{
				storedBuffs.Add(name, buffs[i]);
			}
			else
			{
				storedBuffs.Remove(name);
				storedBuffs.Add(name, buffs[i]);
			}
		}
		// storedBuffs => objects
		int l = 0;
		if(currentSize != previousSize){ //clear list
			foreach(GameObject obj in storedBuffsObjects){
			Image img = storedBuffsObjects[l].GetComponent<Image>();
			BuffItem bi = storedBuffsObjects[l].GetComponent<BuffItem>(); 
			bi.buff = null;
			img.sprite = null;
			img.color = new Color(0,0,0,0);
			l++;
			}
		}
		l = 0;
		foreach(KeyValuePair<string, TimedBuff> pair in storedBuffs)
		{
			BuffItem bi = storedBuffsObjects[l].GetComponent<BuffItem>(); 
			bi.buff = pair.Value;
			Image img = storedBuffsObjects[l].GetComponent<Image>();
			img.color = Color.white;
			l++;
		}
		previousSize = currentSize;
	}

	private void OnHoverBuff(PointerEventData data)
	{
		BuffItem item = data.pointerCurrentRaycast.gameObject.GetComponent<BuffItem>();
		if(item.buff != null){
			toolTip.SetActive(true);
			Text txt = toolTip.transform.GetChild(0).GetComponent<Text>();
			txt.text = item.buff.BuffName;
		}
	}
	private void OnLeaveBuff(PointerEventData data)
	{
		Text txt = toolTip.transform.GetChild(0).GetComponent<Text>();
		txt.text = "";
		toolTip.SetActive(false);
	}
}
