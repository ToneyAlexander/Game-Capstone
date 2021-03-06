﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class positionText : MonoBehaviour
{
    public GameObject player;
    GameObject textObj;
    public GameObject camera;
    public GameObject maritimeController;
    public GameObject info;
    public ThemeDictionary themeDictionary;
    public CrewController crewController;
    private string n;
    private int islandSize = 20;
    private int islandHeight = 5;
    private int islandTerrainType = 1;
    private int themeID = 0;
    private int level = 0;
    private int bossIndex = 0;
    private string[] bosses = { "Beetle", "Dragon" , "Demon", "Ghoul","Wyvern"};
    private string[] terrains = {"Tall", "Flat" ,"Hilly","Cliffy","Pure Flat", "Hilly Flat" };
    private string[] biomeInfos =
    {
        "Grassy Islands contain basic, low-tier body, head, weapon and off-hand items",
        "Deserts contain rings and amulets",
        "Snowy islands contain body and head armor",
        "Swamps contain weapons and off-hands"
    };
    private string[] bossInfos =
    {
        "Beetles typically drop rings and amulets, and may drop unique items",
        "Dragons typically drop body armor, and may drop unique armors",
     
        "Demons typically drop weapons and offhands, and may drop a unique of either",
        "Ghouls typically drop head armor, and may drop a unique head armor",
        "Wyverns are non-functional"
    };
    // Start is called before the first frame update
    Vector3 hiddenPos = new Vector3(-520,0,0);
    Vector3 visiblePos = new Vector3(-300, 0,0);
    void Start()
    {
        islandSize = crewController.selectArea(30);
        islandHeight = (int)Random.Range(3, 6);
        themeID = crewController.selectTheme();
        bossIndex = crewController.selectBoss();
        level = crewController.selectLevel(2);
        islandTerrainType = (int)Random.Range(0, 6);

        // level = 
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        player = players[0];
        for (int i = 0; i < transform.childCount; i++)
        {
            //Debug.Log(transform.GetChild(i).name);
            if (transform.GetChild(i).name.Equals("TextMeshPro"))
            {
               
                //Debug.Log(transform.name +":" +transform.GetChild(i).name);
                textObj = transform.GetChild(i).gameObject;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(player.transform);
        if (textObj != null)
        {
            TextMeshPro g = textObj.GetComponent<TextMeshPro>();
            // Debug.Log(g);
            g.text = n;
        }

       // g.size = 24;
 

        

    }
    public void setName(string name)
    {
        n = name;
    }
    void OnMouseOver()
    {
        TextMeshPro g = textObj.GetComponent<TextMeshPro>();
        g.color = Color.red;
        maritimeController.GetComponent<MaritimeController>().islandName.text = n;
        maritimeController.GetComponent<MaritimeController>().descText.text = "A " + bosses[bossIndex] + " has been spotted on this island!\n" + biomeInfos[themeID] + "\n" + bossInfos[bossIndex] ;
        maritimeController.GetComponent<MaritimeController>().infoText.text = islandSize + "\n" + islandHeight + "\n" + themeDictionary.themeDictionary[themeID] + "\n" + terrains[islandTerrainType] +"\n\n" + level;
  
    }
    void OnMouseDown()
    {
        if (!EventSystem.current.IsPointerOverGameObject() && !BoatCameraController.moving)
           {
            maritimeController.GetComponent<MaritimeController>().islandStorage.name = n;
            maritimeController.GetComponent<MaritimeController>().islandStorage.size = islandSize;
            maritimeController.GetComponent<MaritimeController>().islandStorage.height = islandHeight;
            maritimeController.GetComponent<MaritimeController>().islandStorage.theme = themeID;
            maritimeController.GetComponent<MaritimeController>().islandStorage.boss = bossIndex;
            maritimeController.GetComponent<MaritimeController>().islandStorage.level = level;
            maritimeController.GetComponent<MaritimeController>().islandStorage.terraintype = islandTerrainType;
            BoatCameraController.moving = true;
           }



    }
    void OnMouseExit()
    {
        TextMeshPro g = textObj.GetComponent<TextMeshPro>();
        g.color = Color.black;
        maritimeController.GetComponent<MaritimeController>().descText.text = "Press H to hide/show";
        maritimeController.GetComponent<MaritimeController>().islandName.text = "";
        maritimeController.GetComponent<MaritimeController>().infoText.text = "";
    }
}
