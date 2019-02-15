using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
            Image image = new Image();
            image.transform.parent = content.transform;
            image.sprite = proto.sprite;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
