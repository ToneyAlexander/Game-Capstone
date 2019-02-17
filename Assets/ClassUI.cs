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
    List<PerkPrototype> allPerks;
    List<PerkPrototype> takenPerks;
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
        allPerks = playerClass.allPerks;
        takenPerks = playerClass.takenPerks;
        foreach (PerkPrototype proto in allPerks)
        {
            GameObject image = new GameObject();
            image.transform.parent = content.transform;
            image.name = proto.Name;
            image.AddComponent<Image>();
            //image.GetComponent<Image>.sprite = proto.sprite;
            image.transform.localPosition = new Vector3(proto.uiCoords.x, proto.uiCoords.y, 0);
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
        
    }

    void OnPerkClick(PointerEventData data)
    {

    }
    void OnPerkEnter(PointerEventData data)
    {

    }
    void OnPerkExit(PointerEventData data)
    {

    }
}
