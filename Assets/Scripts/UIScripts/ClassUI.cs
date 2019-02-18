using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using CCC.Stats;

public class ClassUI : MonoBehaviour
{
    GameObject box;
    GameObject content;
    public PlayerClass playerClass;
    PerkPrototype mousedOver;
    bool selected = false;
    GameObject statsBlock;
    Vector3 visibleLoc = new Vector3(-275, 0, 0);
    Vector3 hiddenLoc = new Vector3(-110, 0, 0);
    Text descText;
    // Start is called before the first frame update
    void Start()
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Player");
        if (gameObjects.Length > 0)
        {
            PlayerClass temp = gameObjects[0].GetComponent<PlayerClass>();
            if (temp)
            {
                playerClass = temp;
            }
            else
            {
                TestPlayerClass temp2 = gameObjects[0].GetComponent<TestPlayerClass>();
                if (temp2)
                {
                    playerClass = temp2.GetClass();
                }
            }
        }

        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).name.Equals("ScrollBox"))
            {
                box = gameObject.transform.GetChild(i).gameObject;
                content = box.transform.GetChild(0).GetChild(0).gameObject;
            }
            else if (transform.GetChild(i).name.Equals("StatsBlock"))
            {
                statsBlock = gameObject.transform.GetChild(i).gameObject;
            }

        }
        descText = statsBlock.GetComponentInChildren<Text>();
        //  allPerks = playerClass.allPerks;
        //  takenPerks = playerClass.takenPerks;
        foreach (PerkPrototype proto in playerClass.allPerks)
        {
            GameObject image = new GameObject();
            PerkHolder perkHolder = image.AddComponent<PerkHolder>();
            perkHolder.perkInfo = proto;
            if (proto.Require.Count == 0)
            {
                perkHolder.available = true;
            }
            image.transform.parent = content.transform;
            image.name = proto.Name;
            //RectTransform rect = image.GetComponent<RectTransform>();
            image.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
              // image.width = 30;
            //image.height = 30;
            image.AddComponent<Image>();
            //image.GetComponent<Image>.sprite = proto.sprite;
            image.transform.localPosition = new Vector3(proto.uiCoords.x, -proto.uiCoords.y, 0);
            EventTrigger ev = image.AddComponent<EventTrigger>();

            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerClick;
            entry.callback.AddListener((eventData) => { OnPerkClick((PointerEventData)eventData); });
            ev.triggers.Add(entry);

            EventTrigger.Entry entry2 = new EventTrigger.Entry();
            entry2.eventID = EventTriggerType.PointerEnter;
            entry2.callback.AddListener((eventData) => {
                 OnPerkEnter((PointerEventData)eventData); });
            ev.triggers.Add(entry2);

            EventTrigger.Entry entry3 = new EventTrigger.Entry();
            entry3.eventID = EventTriggerType.PointerExit;
            entry3.callback.AddListener((eventData) => { OnPerkExit((PointerEventData)eventData); });
            ev.triggers.Add(entry3);


        }
    }

    // Update is called once per frame
    void Update()
    {
       for (int i = 0; i <  content.transform.childCount; i++)
        {
            PerkHolder test = content.transform.GetChild(i).gameObject.GetComponent<PerkHolder>();
            Image colorEdit = content.transform.GetChild(i).gameObject.GetComponent<Image>();
            if (!selected)
            {
                statsBlock.transform.localPosition = Vector3.MoveTowards(statsBlock.transform.localPosition, hiddenLoc, 350 * Time.deltaTime);
                
                
                if (test.taken)
                {
                    colorEdit.color = Color.green;
                }
                else if (test.blocked)
                {
                    colorEdit.color = Color.red;
                }
                else if (test.available)
                {
                    colorEdit.color = Color.yellow;
                }
                else
                {
                    colorEdit.color = Color.white;
                }
            }
            else
            {
                colorEdit.color = test.precheck(mousedOver);
                statsBlock.transform.localPosition = Vector3.MoveTowards(statsBlock.transform.localPosition, visibleLoc, 350 * Time.deltaTime);
            }

        }
        
    }

    void OnPerkClick(PointerEventData data)
    {
        Debug.Log("HERE WE GO");
        Debug.Log(data.pointerCurrentRaycast.gameObject);
        PerkHolder clickedEvent = data.pointerCurrentRaycast.gameObject.GetComponent<PerkHolder>();
        // if (Perk)
        if (clickedEvent.available && !clickedEvent.blocked)
        {
            playerClass.TakePerk(clickedEvent.perkInfo);
            for (int i = 0; i < content.transform.childCount; i++)
            {
                PerkHolder test = content.transform.GetChild(i).gameObject.GetComponent<PerkHolder>();
                test.recheck(clickedEvent.perkInfo);
            }
        }

       // foreach(Ga)






    }
    void OnPerkEnter(PointerEventData data)
    {
        
        Debug.Log(data.pointerCurrentRaycast.gameObject);
        PerkHolder clickedEvent = data.pointerCurrentRaycast.gameObject.GetComponent<PerkHolder>();
        mousedOver = clickedEvent.perkInfo;
        descText.text = mousedOver.Name + "\n" + mousedOver.Desc + "\n\n";
        foreach (PerkStatEntry stat in mousedOver.Stats)
        {
            descText.text = descText.text + stat.StatInst.Name + ": " + stat.StatInst.Value + "\n";
        }
        selected = true;


    }
    void OnPerkExit(PointerEventData data)
    {
        Debug.Log("goodbye");
        selected = false;
       // Debug.Log("HERE WE GOOOOOOOO");
       // Debug.Log(data.pointerCurrentRaycast.gameObject);
    }
}
