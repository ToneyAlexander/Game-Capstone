using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ClassUI : MonoBehaviour
{
    GameObject box;
    GameObject content;
    public PlayerClass playerClass;
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

        }
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
       // for ()
        
    }

    void OnPerkClick(PointerEventData data)
    {
        Debug.Log("HERE WE GO");
        Debug.Log(data.pointerCurrentRaycast.gameObject);
        PerkHolder clickedEvent = data.pointerCurrentRaycast.gameObject.GetComponent<PerkHolder>();
       // if (Perk)
        Transform holder = data.pointerCurrentRaycast.gameObject.transform.parent;

        
       



    }
    void OnPerkEnter(PointerEventData data)
    {
        
        Debug.Log(data.pointerCurrentRaycast.gameObject);
        PerkHolder clickedEvent = data.pointerCurrentRaycast.gameObject.GetComponent<PerkHolder>();
        Transform holder = data.pointerCurrentRaycast.gameObject.transform.parent;
        for (int i = 0; i < holder.childCount; i++)
        {
            PerkHolder perkInfo =  holder.GetChild(i).gameObject.GetComponent<PerkHolder>();
            if (perkInfo.taken = false)
            {
                Debug.Log(perkInfo.perkInfo);
            }

        }

    }
    void OnPerkExit(PointerEventData data)
    {
       // Debug.Log("HERE WE GOOOOOOOO");
       // Debug.Log(data.pointerCurrentRaycast.gameObject);
    }
}
