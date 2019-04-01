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
    
    // Start is called before the first frame update
    void Start()
    {
		toolTip.SetActive(false);
		for (int i = 0; i < transform.childCount; i++)
		{
			if (transform.GetChild(i).name.Equals("Column4"))
			{
				column4 = transform.GetChild(i).gameObject;
			}
			else if (transform.GetChild(i).name.Equals("Tooltip"))
			{
				toolTip = transform.GetChild(i).gameObject;
			}
		}

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
		}
	}

    // Update is called once per frame
    void Update()
    {
		// trimming to reduced list
		buffs = player.buffs;
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
		foreach(KeyValuePair<string, TimedBuff> pair in storedBuffs)
		{
			BuffItem inst = storedBuffsObjects[l].GetComponent<BuffItem>();
			inst.buff = pair.Value;
			Image img = storedBuffsObjects[l].GetComponent<Image>();
			//img = inst.buff.Icon;

			//update cooldown
			l++;
		}
	}

	private void OnHoverBuff(PointerEventData data)
	{
		BuffItem item = data.pointerCurrentRaycast.gameObject.GetComponent<BuffItem>();
		toolTip.SetActive(true);
		Text txt = toolTip.transform.GetChild(0).GetComponent<Text>();
		txt.text = item.buff.BuffName;
	}
}
