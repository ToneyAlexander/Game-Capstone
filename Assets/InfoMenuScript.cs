using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InfoMenuScript : MonoBehaviour
{
    GameObject tabs;
    GameObject inventory;
    GameObject stats;
    GameObject classinfo;
    // Start is called before the first frame update

    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).name.Equals("InventoryScreen"))
            {
                inventory = gameObject.transform.GetChild(i).gameObject;
            }
            else if (transform.GetChild(i).name.Equals("StatsScreen"))
            {
                stats = gameObject.transform.GetChild(i).gameObject;
            }
            else if (transform.GetChild(i).name.Equals("ClassScreen"))
            {
               classinfo = gameObject.transform.GetChild(i).gameObject;
            }
            else if (transform.GetChild(i).name.Equals("Tabs"))
            {
                tabs = gameObject.transform.GetChild(i).gameObject;
                
            }
        }
        for (int i = 0; i < tabs.transform.childCount; i++)
        {
          
            if (tabs.transform.GetChild(i).name.Equals("Inventory"))
            {
                EventTrigger ev = tabs.transform.GetChild(i).gameObject.GetComponent<EventTrigger>();
                EventTrigger.Entry entry = new EventTrigger.Entry();
                entry.eventID = EventTriggerType.PointerClick;
                entry.callback.AddListener((eventData) => { OnClickInventory(); });
                ev.triggers.Add(entry);
                
            }
            if (tabs.transform.GetChild(i).name.Equals("StatsSheet"))
            {
              //  Debug.Log("tesrad");
                EventTrigger ev = tabs.transform.GetChild(i).gameObject.GetComponent<EventTrigger>();
                EventTrigger.Entry entry = new EventTrigger.Entry();
                entry.eventID = EventTriggerType.PointerClick;
                entry.callback.AddListener((eventData) => { OnClickStats(); });
                ev.triggers.Add(entry);
               
            }
            if (tabs.transform.GetChild(i).name.Equals("ClassSheet"))
            {
                EventTrigger ev = tabs.transform.GetChild(i).gameObject.GetComponent<EventTrigger>();
                EventTrigger.Entry entry = new EventTrigger.Entry();
                entry.eventID = EventTriggerType.PointerClick;
                entry.callback.AddListener((eventData) => { OnClickClass(); });
                ev.triggers.Add(entry);
                
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    void OnClickInventory()
    {
        Debug.Log("showing Inventory");
        stats.SetActive(false);
        classinfo.SetActive(false);
        inventory.SetActive(true);
    }
    void OnClickClass()
    {
        Debug.Log("showing Class info");
        stats.SetActive(false);
        classinfo.SetActive(true);
        inventory.SetActive(false);

    }
    void OnClickStats()
    {
        Debug.Log("showing Stats");
        stats.SetActive(true);
        classinfo.SetActive(false);
        inventory.SetActive(false);
    }
}
