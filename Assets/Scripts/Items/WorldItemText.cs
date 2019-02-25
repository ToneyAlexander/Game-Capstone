using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldItemText : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.GetComponent<Text>().text = this.transform.parent.parent.GetComponent<WorldItemScript>().item.Name;
    }
}
