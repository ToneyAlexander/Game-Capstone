using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class positionText : MonoBehaviour
{
    public GameObject player;
    GameObject textObj;
    string n;
    // Start is called before the first frame update
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
        Debug.Log(g);
        g.text = n;

    }
    public void setName(string name)
    {
        n = name;

    }
}
