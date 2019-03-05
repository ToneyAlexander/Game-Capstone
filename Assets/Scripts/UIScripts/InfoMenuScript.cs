using UnityEngine;
using UnityEngine.EventSystems;

public class InfoMenuScript : MonoBehaviour
{
    GameObject tabs;
    GameObject inventory;
    GameObject stats;
    GameObject classinfo;
    GameObject abilities;


    #region Toggle Tab Methods
    /// <summary>
    /// Toggles the class info tab on and off.
    /// </summary>
    public void ToggleClassInfoTab()
    {
        inventory.SetActive(false);
        stats.SetActive(false);
        abilities.SetActive(false);

        classinfo.SetActive(!tabs.activeSelf);
        tabs.SetActive(!tabs.activeSelf);
        ClassUI classui = classinfo.GetComponent<ClassUI>();
        classui.updatePlayerClass();
        classui.reloadGraph();
    }

    /// <summary>
    /// Toggles the inventory tab on and off.
    /// </summary>
    public void ToggleInventoryTab()
    {
        inventory.SetActive(!tabs.activeSelf);
        stats.SetActive(false);
        classinfo.SetActive(false);
        abilities.SetActive(false);
        tabs.SetActive(!tabs.activeSelf);
    }

    /// <summary>
    /// Toggles the stats tab on and off.
    /// </summary>
    public void ToggleStatsTab()
    {
        inventory.SetActive(false);
        stats.SetActive(!tabs.activeSelf);
        classinfo.SetActive(false);
        abilities.SetActive(false);
        tabs.SetActive(!tabs.activeSelf);
    }
    #endregion

    #region MonoBehaviour Messages
    private void Start()
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
            else if (transform.GetChild(i).name.Equals("AbilitySheet"))
            {
                Debug.Log("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
                abilities = gameObject.transform.GetChild(i).gameObject;
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
            if (tabs.transform.GetChild(i).name.Equals("AbilitySheet"))
            {
                EventTrigger ev = tabs.transform.GetChild(i).gameObject.GetComponent<EventTrigger>();
                EventTrigger.Entry entry = new EventTrigger.Entry();
                entry.eventID = EventTriggerType.PointerClick;
                entry.callback.AddListener((eventData) => { OnClickAbility(); });
                ev.triggers.Add(entry);

            }

        }
        tabs.SetActive(false);
        stats.SetActive(false);
        inventory.SetActive(false);
        classinfo.SetActive(false);
        abilities.SetActive(false);
    }
    #endregion

    void OnClickInventory()
    {
        Debug.Log("showing Inventory");
        stats.SetActive(false);
        classinfo.SetActive(false);
        inventory.SetActive(true);
        abilities.SetActive(false);
    }
    void OnClickClass()
    {
        Debug.Log("showing Class info");
        stats.SetActive(false);
        classinfo.SetActive(true);
        inventory.SetActive(false);
        abilities.SetActive(false);
        ClassUI classui =  classinfo.GetComponent<ClassUI>();
        classui.updatePlayerClass();
        classui.reloadGraph();

        
    }
    void OnClickStats()
    {
        Debug.Log("showing Stats");
        stats.SetActive(true);
        classinfo.SetActive(false);
        inventory.SetActive(false);
        abilities.SetActive(false);
    }
    void OnClickAbility()
    {
        Debug.Log("showing Inventory");
        stats.SetActive(false);
        classinfo.SetActive(false);
        inventory.SetActive(false);
        abilities.SetActive(true);
    }
}
