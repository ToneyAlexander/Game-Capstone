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
    Text perkPointsText;
    Vector3 visibleLoc = new Vector3(-480, 0, 0);
    Vector3 hiddenLoc = new Vector3(-130, 0, 0);
    Text descText;
	Image spriteImage;
	Text skillTitle;
    bool loaded = false;
    // Start is called before the first frame update
    public void updatePlayerClass()
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
    }
    public void reloadGraph()
    {
        
        if (!loaded)
        {
         //   Debug.Log("aljd;adjk;adj;ad");
            loaded = true;
            foreach (PerkPrototype proto in playerClass.allPerks)
            {
                Vector3 position = new Vector3(proto.uiCoords.x*3, -proto.uiCoords.y*3, 0);
                foreach (PerkPrototype req in proto.Require)
                {
                    Vector3 other = new Vector3(req.uiCoords.x*3, -req.uiCoords.y*3, 0);
                    Vector3 direction = other - position;
                    GameObject line = new GameObject();
                    Image l = line.AddComponent<Image>();
                    line.transform.parent = content.transform;
                    RectTransform rect = line.GetComponent<RectTransform>();
                    line.name = proto.Name + " requires " + req.Name;
                    rect.pivot = new Vector2(0, 0.5f);
                    line.transform.localPosition = position;
                    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                    rect.sizeDelta = new Vector3(direction.magnitude, 2f, 1);
                    rect.Rotate(new Vector3(0, 0, angle));
                    line.transform.localScale = new Vector3(1.0f, line.transform.localScale.y, line.transform.localScale.z);

                    if (proto.RequireAll)
                    {
                        l.color = Color.black;
                    }
                    else
                    {
                        l.color = Color.grey;
                    }

                }
                foreach (PerkPrototype req in proto.BlockedBy)
                {
                    Vector3 other = new Vector3(req.uiCoords.x*3, -req.uiCoords.y*3, 0);
                    Vector3 direction = other - position;
                    GameObject line = new GameObject();
                    Image l = line.AddComponent<Image>();
                    
                    line.transform.parent = content.transform;
                    RectTransform rect = line.GetComponent<RectTransform>();
                    line.name = proto.Name + " is blocked by " + req.Name;
                    rect.pivot = new Vector2(0, 0.5f);
                    line.transform.localPosition = position;
                    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                    rect.sizeDelta = new Vector3(direction.magnitude, 2.0f, 1);
                    line.transform.localScale = new Vector3(1.0f, line.transform.localScale.y, line.transform.localScale.z);
                    rect.Rotate(new Vector3(0, 0, angle));
                    l.color = Color.red;


                }
            }
            foreach (PerkPrototype proto in playerClass.allPerks)
            {

                GameObject image = new GameObject();
                PerkHolder perkHolder = image.AddComponent<PerkHolder>();
                perkHolder.perkInfo = proto;
                perkHolder.playerClass = playerClass;
                if (proto.Require.Count == 0)
                {
                    perkHolder.available = true;
                }
                image.transform.parent = content.transform;
                image.name = proto.Name;
                //RectTransform rect = image.GetComponent<RectTransform>();
                image.transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
                Image spr = image.AddComponent<Image>();
                spr.sprite = proto.sprite;
                //image.GetComponent<Image>.sprite = proto.sprite;
                image.transform.localPosition = new Vector3(proto.uiCoords.x*3, -proto.uiCoords.y*3, 0);
                EventTrigger ev = image.AddComponent<EventTrigger>();

                EventTrigger.Entry entry = new EventTrigger.Entry();
                entry.eventID = EventTriggerType.PointerClick;
                entry.callback.AddListener((eventData) => { OnPerkClick((PointerEventData)eventData); });
                ev.triggers.Add(entry);

                EventTrigger.Entry entry2 = new EventTrigger.Entry();
                entry2.eventID = EventTriggerType.PointerEnter;
                entry2.callback.AddListener((eventData) => {
                    OnPerkEnter((PointerEventData)eventData);
                });
                ev.triggers.Add(entry2);

                EventTrigger.Entry entry3 = new EventTrigger.Entry();
                entry3.eventID = EventTriggerType.PointerExit;
                entry3.callback.AddListener((eventData) => { OnPerkExit((PointerEventData)eventData); });
                ev.triggers.Add(entry3);


            }

        }

    }
    void Awake()
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
				for (int j = 0; j < statsBlock.transform.childCount; j++)
				{
					if (statsBlock.transform.GetChild(j).name.Equals("DescText"))
					{
						descText = statsBlock.transform.GetChild(j).gameObject.GetComponent<Text>();
					}
					else if (statsBlock.transform.GetChild(j).name.Equals("Sprite"))
					{
						spriteImage = statsBlock.transform.GetChild(j).gameObject.GetComponent<Image>();
					}
					else if (statsBlock.transform.GetChild(j).name.Equals("SkillTitle"))
					{
						skillTitle = statsBlock.transform.GetChild(j).gameObject.GetComponent<Text>();
					}
				}
			}
			else if (transform.GetChild(i).name.Equals("PerkPointsHolder"))
			{
				perkPointsText = gameObject.transform.GetChild(i).gameObject.GetComponentInChildren<Text>();
			}

        }
        //  allPerks = playerClass.allPerks;
        //  takenPerks = playerClass.takenPerks;
     //   Debug.Log(playerClass.allPerks);
   
     
    }

    // Update is called once per frame
    void Update()
    {
       for (int i = 0; i <  content.transform.childCount; i++)
        {
            PerkHolder test = content.transform.GetChild(i).gameObject.GetComponent<PerkHolder>();
            Image colorEdit = content.transform.GetChild(i).gameObject.GetComponent<Image>();
            perkPointsText.text = playerClass.PlayerLevelExp.PerkPoints.ToString();
            if (test)
            {
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
        
    }

    void OnPerkClick(PointerEventData data)
    {
   
        PerkHolder clickedEvent = data.pointerCurrentRaycast.gameObject.GetComponent<PerkHolder>();
        // if (Perk)
        bool taken = playerClass.TakePerk(clickedEvent.perkInfo);
        if (taken)
        {
           // Debug.Log(clickedEvent.perkInfo.Name);
            
            for (int i = 0; i < content.transform.childCount; i++)
            {
                PerkHolder test = content.transform.GetChild(i).gameObject.GetComponent<PerkHolder>();
                if (test)
                {
                    test.recheck(clickedEvent.perkInfo);
                }
                
                
            }
        }

       // foreach(Ga)







    }
    void OnPerkEnter(PointerEventData data)
    {
        
      //  Debug.Log(data.pointerCurrentRaycast.gameObject);
        PerkHolder clickedEvent = data.pointerCurrentRaycast.gameObject.GetComponent<PerkHolder>();
        mousedOver = clickedEvent.perkInfo;
		descText.text = mousedOver.Desc + "\n\n";
		skillTitle.text = mousedOver.Name;
		spriteImage.sprite = mousedOver.sprite;
		foreach (PerkStatEntry stat in mousedOver.Stats)
        {
            descText.text = descText.text + stat.StatInst.Name + ": " + stat.StatInst.Value + "\n";
        }
        selected = true;


    }
    void OnPerkExit(PointerEventData data)
    {
       // Debug.Log("goodbye");
        selected = false;
       // Debug.Log("HERE WE GOOOOOOOO");
       // Debug.Log(data.pointerCurrentRaycast.gameObject);
    }
}
