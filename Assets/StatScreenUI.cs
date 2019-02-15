using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatScreenUI : MonoBehaviour
{
    public ControlStatBlock stats;
    // Start is called before the first frame update
    void Start()
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Player");
        if (gameObjects.Length > 0)
        {
            ControlStatBlock temp = gameObjects[0].GetComponent<ControlStatBlock>();
            if (temp)
            {
                stats = temp;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        Text statlist = gameObject.GetComponentInChildren<Text>();
        //Debug.Log(statlist);
        
        statlist.text = "Strength: " + stats.Str + "\n\nDexterity: " + stats.Dex + "\n\nMysticism: " + stats.Myst + "\n\nFortitude: " + stats.Fort;

    }
}
