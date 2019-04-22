using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using CCC.Stats;

public class AfflictionsScript : MonoBehaviour
{

	private Dictionary<string, Affliction> storedAfflictions;

    private Dictionary<Affliction, int> stacks;

	private GameObject[] storedAfflictionsObjects; 

	private List<Affliction> afflictions;
    
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
		storedAfflictions = new Dictionary<string, Affliction>();
        stacks = new Dictionary<Affliction, int>();
		afflictions = new List<Affliction>();
		int count = column4.transform.childCount;
		storedAfflictionsObjects = new GameObject[count];

		GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Player");
        if (gameObjects.Length > 0)
        {
            player = gameObjects[0].GetComponent<ControlStatBlock>();
        }
		
		for(int k = 0; k < column4.transform.childCount; k++)
		{
			storedAfflictionsObjects[k] = column4.transform.GetChild(k).gameObject;
			EventTrigger ev = storedAfflictionsObjects[k].GetComponent<EventTrigger>();
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
        afflictions = player.afflictions;
		currentSize = afflictions.Count;
		storedAfflictions.Clear();
		for (int i = 0; i < afflictions.Count; i++)
		{
			string name = afflictions[i].AfflictionName;
			if (!storedAfflictions.ContainsKey(name))
			{
				storedAfflictions.Add(name, afflictions[i]);
                stacks[afflictions[i]] = 1;
			}
			else
			{
				storedAfflictions.Remove(name);
				storedAfflictions.Add(name, afflictions[i]);
                stacks[afflictions[i]] += 1;
            }
		}
		// storedAfflictions => objects
		int l = 0;
		if(currentSize != previousSize){ //clear list
			foreach(GameObject obj in storedAfflictionsObjects){
			Image img = storedAfflictionsObjects[l].GetComponent<Image>();
            AfflictionItem af = storedAfflictionsObjects[l].GetComponent<AfflictionItem>(); 
			af = null;
                Text stack = storedAfflictionsObjects[l].transform.GetChild(0).GetComponent<Text>();
            stack.text = "";
            img.sprite = null;
			img.color = new Color(0,0,0,0);
			l++;
			}
		}
		l = 0;
		foreach(KeyValuePair<string, Affliction> pair in storedAfflictions)
		{
            AfflictionItem af = storedAfflictionsObjects[l].GetComponent<AfflictionItem>(); 
			af.affliction = pair.Value;
			Image img = storedAfflictionsObjects[l].GetComponent<Image>();
            Text stack = af.transform.GetChild(0).GetComponent<Text>();
            stack.text = stacks[af.affliction].ToString();
            img.color = Color.white;
			img.sprite = af.affliction.Icon;
			l++;
		}
		previousSize = currentSize;
	}

	private void OnHoverBuff(PointerEventData data)
	{
        AfflictionItem item = data.pointerCurrentRaycast.gameObject.GetComponent<AfflictionItem>();
		if(item != null && item.affliction != null){
			toolTip.SetActive(true);
            toolTip.transform.position = data.pointerCurrentRaycast.gameObject.transform.position - new Vector3(150, -75) ;
			Text txt = toolTip.transform.GetChild(0).GetComponent<Text>();
            Text stats = toolTip.transform.GetChild(1).GetComponent<Text>();
            txt.text = item.affliction.AfflictionName;
            foreach (Stat stat in item.affliction.Stats)
            {
                string value = Stat.GetStatString(stat);
                stats.text += stat.Name + ": " + value + "\n";
            }
        }
	}
	private void OnLeaveBuff(PointerEventData data)
	{
		Text txt = toolTip.transform.GetChild(0).GetComponent<Text>();
        Text stats = toolTip.transform.GetChild(1).GetComponent<Text>();
        txt.text = "";
        stats.text = "";
		toolTip.SetActive(false);
	}
}
