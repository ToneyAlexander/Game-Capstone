using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class positionText : MonoBehaviour
{
    public GameObject player;
    GameObject textObj;
    public GameObject camera;
    public GameObject maritimeController;
    public GameObject info;
    private string n;
    private int islandSize = 20;
    private int islandHeight = 5;
    // Start is called before the first frame update
    Vector3 hiddenPos = new Vector3(-520,0,0);
    Vector3 visiblePos = new Vector3(-300, 0,0);
    void Start()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        player = players[0];
        for (int i = 0; i < transform.childCount; i++)
        {
            Debug.Log(transform.GetChild(i).name);
            if (transform.GetChild(i).name.Equals("TextMeshPro"))
            {
               
                Debug.Log(transform.name +":" +transform.GetChild(i).name);
                textObj = transform.GetChild(i).gameObject;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(player.transform);
        
        TextMeshPro g = textObj.GetComponent<TextMeshPro>();
       // Debug.Log(g);
        g.text = n;
 

        

    }
    public void setName(string name)
    {
        n = name;
    }
    void OnMouseOver()
    {
        TextMeshPro g = textObj.GetComponent<TextMeshPro>();
        g.color = Color.red;
        maritimeController.GetComponent<MaritimeController>().guideText = n + "\n Area: "+ islandSize + "\n Elevation: "+ islandHeight+  " \n\n\n A mysterious presence haunts this island...";
    }
    void OnMouseDown()
    {
        maritimeController.GetComponent<MaritimeController>().islandStorage.name = n;
        maritimeController.GetComponent<MaritimeController>().islandStorage.size = islandSize;
        maritimeController.GetComponent<MaritimeController>().islandStorage.height = islandHeight;


    }
    void OnMouseExit()
    {
        TextMeshPro g = textObj.GetComponent<TextMeshPro>();
        g.color = Color.black;
        maritimeController.GetComponent<MaritimeController>().guideText = "Press H to hide/show";
        // info.transform.GetChild(0).gameObject.GetComponent<Text>().text = "Press H to hide/show";
    }
}
